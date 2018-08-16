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
        readonly IEntityManager entities;
        readonly IInventoryManager inventory;
        readonly IWorldManager world;
        readonly IGameManager game;

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

        public GameplayScreen()
        {
            entities = new EntityManager();
            inventory = new InventoryManager(entities);
            world = new WorldManager();
            game = new GameManager(entities, inventory, world);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {

            entities.LoadContent();
            inventory.LoadContent();
            world.LoadContent();
            game.LoadContent();

            camera = new Camera();

            player = entities.GetPlayer();

            worldView = new GuiWorld(entities, world, game);
            worldView.AssociateCamera(camera);

            Minimap = new GuiMinimap(entities, world, game)
            {
                Size = new Size2D(224, 176)
            };
            SideBar = new GuiSideBar(entities, inventory, game)
            {
                Size = new Size2D(240, 326)
            };

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
