using Microsoft.Xna.Framework;
using NuciXNA.Gui;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Gui.GuiElements;
using Craftico.Models;

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
        /// Gets or sets the minimap.
        /// </summary>
        /// <value>The minimap.</value>
        public GuiMinimap Minimap { get; set; }

        /// <summary>
        /// Gets or sets the minimap.
        /// </summary>
        /// <value>The minimap.</value>
        public GuiSideBar SideBar { get; set; }

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

            Minimap = new GuiMinimap { Size = new Size2D(224, 176) };
            SideBar = new GuiSideBar { Size = new Size2D(240, 334) };

            Minimap.AssociateGameManager(game);

            playerImage = new GuiMob();
            playerImage.AssociateMob(player);

            GuiManager.Instance.GuiElements.Add(world);
            GuiManager.Instance.GuiElements.Add(playerImage);
            GuiManager.Instance.GuiElements.Add(Minimap);
            GuiManager.Instance.GuiElements.Add(SideBar);

            base.LoadContent();

            SideBar.AssociateGameManager(game);
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

            SideBar.Location = new Point2D(
                ScreenManager.Instance.Size.Width - SideBar.Size.Width,
                ScreenManager.Instance.Size.Height - SideBar.Size.Height);

            playerImage.Location = new Point2D(
                world.Size.Width / 2 - 1,
                world.Size.Height / 2 - 1);

            Minimap.Location = new Point2D(ScreenManager.Instance.Size.Width - Minimap.Size.Width, 0);
            camera.CentreOnLocation(player.Location);

            base.SetChildrenProperties();
        }
    }
}
