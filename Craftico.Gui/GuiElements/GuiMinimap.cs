using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.DataAccess.Resources;
using NuciXNA.Graphics.Drawing;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;
using NuciXNA.Primitives.Mapping;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;

namespace Craftico.Gui.GuiElements
{
    public class GuiMinimap : GuiElement
    {
        readonly IEntityManager entities;
        readonly IWorldManager world;
        readonly IGameManager game;

        Mob player;

        GuiMinimapIndicator healthIndicator;
        GuiMinimapIndicator staminaIndiator;
        GuiMinimapIndicator manaIndicator;

        byte[,] alphaMask;

        TextureSprite mobDot;
        TextureSprite pixel;
        TextureSprite frame;

        public Rectangle2D MinimapArea
        {
            get
            {
                int minimapSize = 135;

                return new Rectangle2D(
                    (Size.Width - minimapSize) / 2,
                    (Size.Height - minimapSize) / 2,
                    minimapSize,
                    minimapSize);
            }
        }

        public Rectangle2D DisplayedWorldView
        {
            get
            {
                return new Rectangle2D(
                    (int)player.Location.X - MinimapArea.Width / 2 / ZoomLevel,
                    (int)player.Location.Y - MinimapArea.Height / 2 / ZoomLevel,
                    MinimapArea.Size / ZoomLevel);
            }
        }

        public bool IsClickable { get; set; }

        public int ZoomLevel { get; set; }

        public GuiMinimap(
            IEntityManager entities,
            IWorldManager world,
            IGameManager game)
        {
            this.entities = entities;
            this.world = world;
            this.game = game;

            IsClickable = true;
            ZoomLevel = 2;
        }

        public override void LoadContent()
        {
            mobDot = new TextureSprite
            {
                ContentFile = "Interface/Minimap/entity_dot"
            };
            pixel = new TextureSprite
            {
                ContentFile = "ScreenManager/FillImage",
                Scale = new Scale2D(ZoomLevel)
            };
            frame = new TextureSprite
            {
                ContentFile = "Interface/Minimap/frame"
            };

            healthIndicator = new GuiMinimapIndicator
            {
                BackgroundColour = Colour.PersianRed,
                Icon = "Interface/Minimap/icon_health"
            };
            staminaIndiator = new GuiMinimapIndicator
            {
                BaseValue = 100,
                BackgroundColour = Colour.OliveDrab,
                Icon = "Interface/Minimap/icon_stamina"
            };
            manaIndicator = new GuiMinimapIndicator
            {
                BackgroundColour = Colour.CornflowerBlue,
                Icon = "Interface/Minimap/icon_mana"
            };

            Texture2D maskTexture = ResourceManager.Instance.LoadTexture2D("Interface/Minimap/mask");
            Color[] maskBits = new Color[maskTexture.Width * maskTexture.Height];
            maskTexture.GetData(maskBits, 0, maskBits.Length);

            alphaMask = new byte[Size.Width, Size.Height];

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    int i = x + y * Size.Width;

                    alphaMask[x, y] = maskBits[i].R;
                }
            }

            mobDot.LoadContent();
            pixel.LoadContent();
            frame.LoadContent();

            AddChild(healthIndicator);
            AddChild(staminaIndiator);
            AddChild(manaIndicator);

            player = entities.GetPlayer();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mobDot.Update(gameTime);
            pixel.Update(gameTime);
            frame.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawMinimapTerrain(spriteBatch);
            DrawMinimapEntities(spriteBatch);
            frame.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            frame.Location = Location;

            healthIndicator.CurrentValue = player.Vigour.CurrentLevel;
            healthIndicator.BaseValue = player.Vigour.BaseLevel;
            healthIndicator.Location = new Point2D(Location.X + 17, Location.Y + 36);

            staminaIndiator.CurrentValue = 80;
            staminaIndiator.Location = new Point2D(Location.X + 162, Location.Y + 146);

            manaIndicator.CurrentValue = player.Faith.CurrentLevel;
            manaIndicator.BaseValue = player.Faith.BaseLevel;
            manaIndicator.Location = new Point2D(Location.X + 10, Location.Y + 72);
        }

        void DrawMinimapTerrain(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < DisplayedWorldView.Height; y++)
            {
                for (int x = 0; x < DisplayedWorldView.Width; x++)
                {
                    Point2D location = new Point2D(x * ZoomLevel, y * ZoomLevel);

                    WorldTile tile = world.GetTile(DisplayedWorldView.X + x + 1, DisplayedWorldView.Y + y + 1);
                    Terrain terrain = entities.GetTerrain(tile.TerrainId);

                    Colour terrainColour = Colour.Black;

                    if (tile.WorldObjectId != null)
                    {
                        WorldObject worldObject = entities.GetWorldObject(tile.WorldObjectId);
                        terrainColour = worldObject.Colour;
                    }
                    else if (terrain != null)
                    {
                        terrainColour = terrain.Colour;
                    }

                    DrawMinimapPixel(spriteBatch, terrainColour, location);
                }
            }
        }

        void DrawMinimapEntities(SpriteBatch spriteBatch)
        {
            foreach (Mob mob in entities.GetMobs())
            {
                Point2D mobLocation = new Point2D((int)mob.Location.X, (int)mob.Location.Y);

                if (!DisplayedWorldView.Contains(mobLocation))
                {
                    continue;
                }

                Point2D location = (mobLocation - DisplayedWorldView.Location) * ZoomLevel;

                DrawMinimapDot(spriteBatch, location, Colour.Yellow);
            }
        }

        void DrawMinimapDot(SpriteBatch spriteBatch, Point2D location, Colour colour)
        {
            Point2D screenLocation = Location + MinimapArea.Location + location + new Point2D(mobDot.SpriteSize / 2);

            if (!ClientRectangle.Contains(screenLocation))
            {
                return;
            }

            mobDot.Tint = colour;
            mobDot.Location = screenLocation;

            mobDot.Draw(spriteBatch);
        }

        void DrawMinimapPixel(SpriteBatch spriteBatch, Colour colour, Point2D location)
        {
            Point2D elementLocation = MinimapArea.Location + location;
            Point2D screenLocation = Location + elementLocation;

            if (!ClientRectangle.Contains(screenLocation))
            {
                return;
            }

            int alpha = alphaMask[elementLocation.X, elementLocation.Y];

            if (alpha == 0)
            {
                return;
            }

            pixel.Tint = colour;
            pixel.Location = screenLocation;

            // TODO: Opacity changing doesn't work properly
            //pixel.Opacity = alphaMask[screenLocation.X - Location.X, screenLocation.Y - Location.Y];

            pixel.Draw(spriteBatch);
        }
    }
}
