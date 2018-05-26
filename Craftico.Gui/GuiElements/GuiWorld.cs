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
        Sprite sprite;
        TerrainSpriteSheetEffect terrainEffect;

        public override void LoadContent()
        {
            terrainEffect = new TerrainSpriteSheetEffect();
            sprite = new Sprite
            {
                ContentFile = $"SpriteSheets/Terrains/grass",
                SpriteSheetEffect = terrainEffect,
                Active = true
            };
            sprite.LoadContent();

            sprite.SpriteSheetEffect.AssociateSprite(sprite);
            sprite.SpriteSheetEffect.Activate();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            sprite.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point camCoordsBegin = new Point(camera.Location.X / GameDefines.MAP_TILE_SIZE,
                                             camera.Location.Y / GameDefines.MAP_TILE_SIZE);
            Point camCoordsEnd = new Point(camCoordsBegin.X + camera.Size.Width / GameDefines.MAP_TILE_SIZE,
                                           camCoordsBegin.Y + camera.Size.Height / GameDefines.MAP_TILE_SIZE);

            for (int y = camCoordsBegin.Y - 1; y <= camCoordsEnd.Y + 1; y++)
            {
                for (int x = camCoordsBegin.X - 1; x <= camCoordsEnd.X + 1; x++)
                {
                    WorldTile tile = game.GetTile(x, y);

                    // TODO: Update content file

                    terrainEffect.Variation = TerrainVariation.RegularDetailedHigh;
                    sprite.Location = new Point2D(
                        tile.WorldLocation.X * GameDefines.MAP_TILE_SIZE - camera.Location.X,
                        tile.WorldLocation.Y * GameDefines.MAP_TILE_SIZE - camera.Location.Y);

                    sprite.Draw(spriteBatch);
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
