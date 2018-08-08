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
        IEntityManager entities;
        IGameManager game;
        IWorldManager world;

        Camera camera;
        Mob player;

        GuiWorld worldView;

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
            entities = new EntityManager();
            world = new WorldManager();
            game = new GameManager(entities, world);

            entities.LoadContent();
            world.LoadContent();
            game.LoadContent();

            camera = new Camera();

            player = game.GetPlayer();

            worldView = new GuiWorld();
            worldView.AssociateGameManager(game);
            worldView.AssociateCamera(camera);

            Minimap = new GuiMinimap
            {
                Size = new Size2D(224, 176)
            };
            SideBar = new GuiSideBar(entities, game)
            {
                Size = new Size2D(240, 326)
            };

            Minimap.AssociateGameManager(game);

            GuiManager.Instance.GuiElements.Add(worldView);
            GuiManager.Instance.GuiElements.Add(Minimap);
            GuiManager.Instance.GuiElements.Add(SideBar);

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            entities.UnloadContent();
            world.UnloadContent();
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
            entities.Update(gameTime);
            world.Update(gameTime);
            game.Update(gameTime);
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void SetChildrenProperties()
        {
            worldView.Size = ScreenManager.Instance.Size;

            SideBar.Location = new Point2D(
                ScreenManager.Instance.Size.Width - SideBar.Size.Width,
                ScreenManager.Instance.Size.Height - SideBar.Size.Height);

            Minimap.Location = new Point2D(ScreenManager.Instance.Size.Width - Minimap.Size.Width, 0);
            camera.CentreOnLocation(player.Location);

            base.SetChildrenProperties();
        }
    }
}
