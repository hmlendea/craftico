using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    /// <summary>
    /// Game manager.
    /// </summary>
    public class GameManager : IGameManager
    {
        Mob player;

        IEntityManager entityManager;
        IInventoryManager inventoryManager;
        IWorldManager worldManager;

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns>The player.</returns>
        public Mob GetPlayer()
        => player;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            player = new Mob
            {
                Vigour = new Skill(10, 1154),
                Inventory = new Inventory(28)
                {
                    CuirassSlot = new InventorySlot
                    {
                        ItemId = "metal_cuirass",
                        Quantity = 1
                    },
                    GreavesSlot = new InventorySlot
                    {
                        ItemId = "metal_greaves",
                        Quantity = 1
                    },
                    GlovesSlot = new InventorySlot
                    {
                        ItemId = "metal_gloves",
                        Quantity = 1
                    },
                    BootsSlot = new InventorySlot
                    {
                        ItemId = "metal_boots",
                        Quantity = 1
                    }
                }
            };

            entityManager = new EntityManager();
            inventoryManager = new InventoryManager(player, entityManager);
            worldManager = new WorldManager();

            entityManager.LoadContent();
            inventoryManager.LoadContent();
            worldManager.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            entityManager.UnloadContent();
            inventoryManager.UnloadContent();
            worldManager.UnloadContent();
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {
            HandleMovement();
            HandleInteractions();

            entityManager.Update(gameTime);
            inventoryManager.Update(gameTime);
            worldManager.Update(gameTime);
        }

        public WorldTile GetTile(int x, int y) => worldManager.GetTile(x, y);

        public Terrain GetTerrain(string id) => entityManager.GetTerrain(id);

        public void MovePlayer(MobDirection direction)
        {
            player.Action = MobAction.Movement;
            player.Direction = direction;

            float deltaX = 0.0f;
            float deltaY = 0.0f;

            switch (direction)
            {
                case MobDirection.North:
                    deltaY = -player.MovementSpeed;
                    break;

                case MobDirection.West:
                    deltaX = -player.MovementSpeed;
                    break;

                case MobDirection.South:
                    deltaY = player.MovementSpeed;
                    break;

                case MobDirection.East:
                    deltaX = player.MovementSpeed;
                    break;
            }

            player.Location = new PointF2D(
                player.Location.X + deltaX,
                player.Location.Y + deltaY);
        }

        void HandleMovement()
        {
            if (InputManager.Instance.IsKeyDown(Keys.W))
            {
                MovePlayer(MobDirection.North);
            }
            else if (InputManager.Instance.IsKeyDown(Keys.A))
            {
                MovePlayer(MobDirection.West);
            }
            else if (InputManager.Instance.IsKeyDown(Keys.S))
            {
                MovePlayer(MobDirection.South);
            }
            else if (InputManager.Instance.IsKeyDown(Keys.D))
            {
                MovePlayer(MobDirection.East);
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
