using Microsoft.Xna.Framework;
using NuciXNA.Gui;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Gui.GuiElements;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.Screens
{
    /// <summary>
    /// Gameplay screen.
    /// </summary>
    public class GameplayScreen : Screen
    {
        IGameManager game;

        Camera camera;
        Mob player;

        GuiWorld world;
        GuiMob playerImage;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            game = new GameManager();
            camera = new Camera();

            game.LoadContent();

            player = game.GetPlayer();

            world = new GuiWorld();
            world.AssociateGameManager(game);
            world.AssociateCamera(camera);

            playerImage = new GuiMob();
            playerImage.AssociateMob(player);

            GuiManager.Instance.GuiElements.Add(world);
            GuiManager.Instance.GuiElements.Add(playerImage);

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            game.UnloadContent();
            camera.UnloadContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            game.Update(gameTime);
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void SetChildrenProperties()
        {
            world.Size = ScreenManager.Instance.Size;

            playerImage.Location = new Point2D(
                (int)(player.Location.X * GameDefines.MAP_TILE_SIZE),
                (int)(player.Location.Y * GameDefines.MAP_TILE_SIZE));

            base.SetChildrenProperties();
        }
    }
}
