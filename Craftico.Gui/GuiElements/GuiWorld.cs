using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.Graphics;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Input;
using NuciXNA.Input.Enumerations;
using NuciXNA.Input.Events;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Gui.MobAnimationEffects;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiWorld : GuiElement
    {
        IGameManager game;
        Camera camera;
        Mob player;

        Sprite[,] sprites;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public override void LoadContent()
        {
            player = game.GetPlayer();

            Rows = Size.Height / GameDefines.MAP_TILE_SIZE + 2;
            Columns = Size.Width / GameDefines.MAP_TILE_SIZE + 2;

            sprites = new Sprite[Columns, Rows];

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Sprite sprite = new Sprite
                    {
                        ContentFile = $"SpriteSheets/Terrains/grass",
                        SpriteSheetEffect = new TerrainSpriteSheetEffect
                        {
                            Variation = TerrainVariation.RegularDetailedMedium
                        },
                        Active = true
                    };
                    sprite.LoadContent();

                    sprite.Location = new Point2D(x * GameDefines.MAP_TILE_SIZE, y * GameDefines.MAP_TILE_SIZE);

                    sprite.SpriteSheetEffect.AssociateSprite(sprite);
                    sprite.SpriteSheetEffect.Activate();

                    sprites[x, y] = sprite;
                }
            }

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    sprites[x, y].UnloadContent();
                }
            }

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Size2D off = new Size2D(
                (int)((camera.Location.X - (int)camera.Location.X) * GameDefines.MAP_TILE_SIZE),
                (int)((camera.Location.Y - (int)camera.Location.Y) * GameDefines.MAP_TILE_SIZE));

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Sprite sprite = sprites[x, y];
                    WorldTile tile = game.GetTile(
                        (int)camera.Location.X + x,
                        (int)camera.Location.Y + y);

                    string contentFile = $"SpriteSheets/Terrains/{tile.TerrainId}";

                    if (sprite.ContentFile != contentFile)
                    {
                        sprite.ContentFile = contentFile;
                        sprite.LoadContent();
                    }

                    sprite.Location = new Point2D(
                        (x - 1) * GameDefines.MAP_TILE_SIZE - off.Width,
                        (y - 1) * GameDefines.MAP_TILE_SIZE - off.Height);
                    sprite.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    sprites[x, y].Draw(spriteBatch);
                }
            }

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
    }
}
