using Microsoft.Xna.Framework.Input;
using NuciXNA.Gui;
using NuciXNA.Gui.Screens;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Gui.GuiElements;
using Craftico.Models;
using Microsoft.Xna.Framework;

namespace Craftico.Gui.Screens
{
    /// <summary>
    /// Gameplay screen.
    /// </summary>
    public class GameplayScreen : Screen
    {
        IGameManager game;

        Mob player;

        GuiMob playerImage;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            game = new GameManager();

            game.LoadContent();

            player = game.GetPlayer();

            playerImage = new GuiMob
            {
                Location = new Point2D(100, 100)
            };
            playerImage.AssociateMob(player);

            GuiManager.Instance.GuiElements.Add(playerImage);

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            game.UnloadContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            game.Update(gameTime);

            base.Update(gameTime);
        }
    }
}
