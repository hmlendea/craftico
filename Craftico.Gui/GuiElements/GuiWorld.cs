using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.Graphics.Drawing;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Gui.MobAnimationEffects;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiWorld : GuiElement
    {
        IEntityManager entities;
        IWorldManager world;
        IGameManager game;
        Camera camera;
        Mob player;

        Dictionary<string, Sprite> tileSprites;
        Dictionary<string, GuiWorldObject> worldObjects;

        GuiMob playerImage;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public GuiWorld(
            IEntityManager entities,
            IWorldManager world,
            IGameManager game)
        {
            this.entities = entities;
            this.world = world;
            this.game = game;
        }

        public override void LoadContent()
        {
            player = entities.GetPlayer();
            playerImage = new GuiMob();
            playerImage.AssociateMob(player);

            Rows = Size.Height / GameDefines.MAP_TILE_SIZE + 2;
            Columns = Size.Width / GameDefines.MAP_TILE_SIZE + 2;

            LoadTileSprites();
            LoadWorldObjects();

            AddChild(playerImage);

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            foreach (Sprite sprite in tileSprites.Values)
            {
                sprite.UnloadContent();
            }

            foreach (GuiWorldObject worldObjectImage in worldObjects.Values)
            {
                worldObjectImage.UnloadContent();
            }

            tileSprites.Clear();
            tileSprites = null;

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in tileSprites.Values)
            {
                sprite.Update(gameTime);
            }

            foreach (GuiWorldObject worldObjectImage in worldObjects.Values)
            {
                worldObjectImage.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTerrain(spriteBatch);
            DrawWorldObjects(spriteBatch);

            base.Draw(spriteBatch);
        }

        public void AssociateGameManager(IGameManager game)
        {
            this.game = game;
        }

        public void AssociateCamera(Camera camera)
        {
            this.camera = camera;
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            playerImage.Location = new Point2D(Size / 2);
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();

            InputManager.Instance.MouseButtonPressed += this_MouseButtonPressed;
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();

            InputManager.Instance.MouseButtonPressed -= this_MouseButtonPressed;
        }

        void this_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.LeftButton)
            {
                game.MovePlayer(ScreenToMapCoordinates(e.Location));
            }
        }

        /// <summary>
        /// Gets the map cpprdomates based on the specified screen coordinates.
        /// </summary>
        /// <returns>The map coordinates.</returns>
        /// <param name="screenCoords">Screen coordinates.</param>
        Point2D ScreenToMapCoordinates(Point2D screenCoords)
        {
            return new Point2D(
                (int)camera.Location.X + screenCoords.X / GameDefines.MAP_TILE_SIZE,
                (int)camera.Location.Y + screenCoords.Y / GameDefines.MAP_TILE_SIZE);
        }

        void LoadTileSprites()
        {
            tileSprites = new Dictionary<string, Sprite>();

            foreach (Terrain terrain in entities.GetTerrains())
            {
                TextureSprite sprite = new TextureSprite
                {
                    ContentFile = $"SpriteSheets/Terrains/{terrain.SpriteSheet}",
                    SourceRectangle = new Rectangle2D(Point2D.Empty, GameDefines.MAP_TILE_SIZE, GameDefines.MAP_TILE_SIZE),
                    SpriteSheetEffect = new TerrainSpriteSheetEffect
                    {
                        Variation = TerrainVariation.RegularEmpty
                    },
                    Active = true
                };

                sprite.LoadContent();
                sprite.SpriteSheetEffect.Activate();

                if (!tileSprites.ContainsKey(terrain.SpriteSheet))
                {
                    tileSprites.Add(terrain.SpriteSheet, sprite);
                }
            }
        }

        void LoadWorldObjects()
        {
            worldObjects = new Dictionary<string, GuiWorldObject>();

            foreach (WorldObject worldObject in entities.GetWorldObjects())
            {
                GuiWorldObject worldObjectImage = new GuiWorldObject(entities, worldObject.Id);
                worldObjectImage.LoadContent();

                worldObjects.Add(worldObject.Id, worldObjectImage);
            }
        }

        void DrawTerrain(SpriteBatch spriteBatch)
        {
            Size2D off = new Size2D(
                (int)((camera.Location.X - (int)camera.Location.X) * GameDefines.MAP_TILE_SIZE),
                (int)((camera.Location.Y - (int)camera.Location.Y) * GameDefines.MAP_TILE_SIZE));

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    WorldTile tile = world.GetTile(
                        (int)camera.Location.X + x,
                        (int)camera.Location.Y + y);
                    Sprite sprite = tileSprites[tile.TerrainId];

                    sprite.Location = new Point2D(
                        (x - 1) * GameDefines.MAP_TILE_SIZE - off.Width,
                        (y - 1) * GameDefines.MAP_TILE_SIZE - off.Height);
                    sprite.Draw(spriteBatch);
                }
            }
        }

        void DrawWorldObjects(SpriteBatch spriteBatch)
        {
            Size2D off = new Size2D(
                (int)((camera.Location.X - (int)camera.Location.X) * GameDefines.MAP_TILE_SIZE),
                (int)((camera.Location.Y - (int)camera.Location.Y) * GameDefines.MAP_TILE_SIZE));

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    WorldTile tile = world.GetTile(
                        (int)camera.Location.X + x,
                        (int)camera.Location.Y + y);

                    if (tile.WorldObjectId == null)
                    {
                        continue;
                    }

                    GuiWorldObject worldObjectImage = worldObjects[tile.WorldObjectId];
                    worldObjectImage.Location = new Point2D(
                        (x - 1) * GameDefines.MAP_TILE_SIZE - off.Width,
                        (y - 1) * GameDefines.MAP_TILE_SIZE - off.Height);
                    worldObjectImage.Draw(spriteBatch);
                }
            }
        }
    }
}
