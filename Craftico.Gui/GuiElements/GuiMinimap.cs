using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.DataAccess.Resources;
using NuciXNA.Graphics;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;
using NuciXNA.Primitives.Mapping;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;

namespace Craftico.Gui.GuiElements
{
    public class GuiMinimap : GuiElement
    {
        IGameManager game;

        GuiMinimapIndicator healthIndicator;
        GuiMinimapIndicator staminaIndiator;
        GuiMinimapIndicator manaIndicator;

        byte[,] alphaMask;

        Sprite mapMarkerSprite;
        Sprite mobDot;
        Sprite pixel;
        Sprite frame;

        public bool IsClickable { get; set; }

        public int ZoomLevel { get; set; }

        public GuiMinimap()
        {
            IsClickable = true;
            ZoomLevel = 2;
        }

        public override void LoadContent()
        {
            mapMarkerSprite = new Sprite { ContentFile = "Interface/Minimap/markers" };
            mobDot = new Sprite { ContentFile = "Interface/Minimap/entity_dot" };
            pixel = new Sprite
            {
                ContentFile = "ScreenManager/FillImage",
                Scale = new Scale2D(ZoomLevel, ZoomLevel)
            };
            frame = new Sprite { ContentFile = "Interface/Minimap/frame" };

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

            mapMarkerSprite.LoadContent();
            mobDot.LoadContent();
            pixel.LoadContent();
            frame.LoadContent();

            Children.Add(healthIndicator);
            Children.Add(staminaIndiator);
            Children.Add(manaIndicator);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mapMarkerSprite.Update(gameTime);
            mobDot.Update(gameTime);
            pixel.Update(gameTime);
            frame.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Mob player = game.GetPlayer();

            Point2D startLocation = new Point2D(
                (int)player.Location.X - Size.Width / (2 * ZoomLevel),
                (int)player.Location.Y - Size.Height / (2 * ZoomLevel));

            DrawMinimapTerrain(spriteBatch, startLocation);
            DrawMinimapEntities(spriteBatch);
            frame.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public void AssociateGameManager(IGameManager game)
        {
            this.game = game;
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            Mob player = game.GetPlayer();

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

        void DrawMinimapTerrain(SpriteBatch spriteBatch, Point2D startLocation)
        {
            for (int y = 0; y < Size.Height / ZoomLevel; y++)
            {
                for (int x = 0; x < Size.Width / ZoomLevel; x++)
                {
                    Point2D screenLocation = new Point2D(
                        Location.X + x * ZoomLevel,
                        Location.Y + y * ZoomLevel);

                    WorldTile tile = game.GetTile(startLocation.X + x, startLocation.Y + y);
                    Terrain terrain = game.GetTerrain(tile.TerrainId);

                    Colour terrainColour = terrain != null ? terrain.Colour : Colour.Black;

                    DrawMinimapPixel(spriteBatch, terrainColour, screenLocation);
                }
            }
        }

        void DrawMinimapEntities(SpriteBatch spriteBatch)
        {
            Point2D location = new Point2D(
                Location.X + Size.Width / 2,
                Location.Y + Size.Height / 2);

            DrawMinimapDot(spriteBatch, location, Colour.White);
        }

        void DrawMinimapTiles(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    Colour tileColour = Colour.Black;
                    int alpha = tileColour.A - 255 + alphaMask[x, y];

                    pixel.Location = new Point2D(Location.X + x, Location.Y + y);
                    pixel.Tint = Color.FromNonPremultiplied(tileColour.R, tileColour.G, tileColour.B, alpha).ToColour();

                    pixel.Draw(spriteBatch);
                }
            }
        }

        void DrawMinimapDot(SpriteBatch spriteBatch, Point2D dotLocation, Colour colour)
        {
            if (dotLocation.X < ClientRectangle.Left ||
                dotLocation.Y < ClientRectangle.Top ||
                dotLocation.X >= ClientRectangle.Right ||
                dotLocation.Y >= ClientRectangle.Bottom)
            {
                return;
            }

            mobDot.Tint = colour;
            mobDot.Location = new Point2D(
                dotLocation.X - mobDot.SpriteSize.Width / 2,
                dotLocation.Y - mobDot.SpriteSize.Height / 2);

            mobDot.Draw(spriteBatch);
        }

        void DrawMinimapPixel(SpriteBatch spriteBatch, Colour colour, Point2D screenLocation)
        {
            if (!ClientRectangle.Contains(screenLocation))
            {
                return;
            }

            int alpha = alphaMask[screenLocation.X - Location.X, screenLocation.Y - Location.Y];

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
