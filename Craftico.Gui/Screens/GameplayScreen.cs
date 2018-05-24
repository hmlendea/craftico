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
            player = game.GetPlayer();

            playerImage = new GuiMob
            {
                Location = new Point2D(100, 100)
            };
            playerImage.AssociateMob(player);

            GuiManager.Instance.GuiElements.Add(playerImage);
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            HandleMovement();
            HandleInteractions();

            base.Update(gameTime);
        }

        void HandleMovement()
        {
            if (InputManager.Instance.IsKeyDown(Keys.W))
            {
                player.Action = MobAction.Movement;
                player.Direction = MobDirection.North;
            }
            else if (InputManager.Instance.IsKeyDown(Keys.A))
            {
                player.Action = MobAction.Movement;
                player.Direction = MobDirection.West;
            }
            else if (InputManager.Instance.IsKeyDown(Keys.S))
            {
                player.Action = MobAction.Movement;
                player.Direction = MobDirection.South;
            }
            else if (InputManager.Instance.IsKeyDown(Keys.D))
            {
                player.Action = MobAction.Movement;
                player.Direction = MobDirection.East;
            }
            else
            {
                player.Action = MobAction.Idle;
            }
        }

        void HandleInteractions()
        {
            if (player.Action == MobAction.Movement)
            {
                return;
            }

            if (InputManager.Instance.IsKeyDown(Keys.X))
            {
                player.Action = MobAction.MeleeFighting;
            }
            else if (InputManager.Instance.IsKeyDown(Keys.Z))
            {
                player.Action = MobAction.RangedFighting;
            }
            else if (InputManager.Instance.IsKeyDown(Keys.C))
            {
                player.Action = MobAction.SpellCasting;
            }
            else
            {
                player.Action = MobAction.Idle;
            }
        }
    }
}
