using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NuciXNA.Graphics;
using NuciXNA.Gui.GuiElements;
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

        Sprite[,] sprites;

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public override void LoadContent()
        {
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
            Point camCoordsBegin = new Point(camera.Location.X / GameDefines.MAP_TILE_SIZE,
                                             camera.Location.Y / GameDefines.MAP_TILE_SIZE);

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    WorldTile tile = game.GetTile(camCoordsBegin.X + x, camCoordsBegin.Y + y);
                    Sprite sprite = sprites[x, y];

                    string contentFile = $"SpriteSheets/Terrains/{tile.TerrainId}";

                    if (sprite.ContentFile != contentFile)
                    {
                        sprite.ContentFile = contentFile;
                        sprite.LoadContent();
                    }

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
    }
}
