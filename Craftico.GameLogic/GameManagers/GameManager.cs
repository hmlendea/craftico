using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.PathFinding;
using Craftico.Models;
using Craftico.Settings;

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

        float baseMovementSpeed;

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

            baseMovementSpeed = (float)GameDefines.MAP_TILE_SIZE / 5120;

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

        Path path;
        public void MovePlayer(Point2D destination)
        {
            Point2D start = new Point2D((int)player.Location.X, (int)player.Location.Y);

            path = PathFinder.GetShortestPath(start, destination, worldManager);
        }

        void HandleMovement()
        {
            if (Path.IsNullOrEmpty(path))
            {
                player.Action = MobAction.Idle;
                return;
            }

            player.Action = MobAction.Movement;

            float newX = player.Location.X;
            float newY = player.Location.Y;
            float walkDistance = baseMovementSpeed * player.MovementSpeed;

            if (path.FirstNode.Location.Y < player.Location.Y)
            {
                player.Direction = MobDirection.North;
                newY -= walkDistance;

                if (newY <= path.FirstNode.Location.Y)
                {
                    newY = path.FirstNode.Location.Y;
                    path.RemoveAt(0);
                }
            }
            else if (path.FirstNode.Location.X < player.Location.X)
            {
                player.Direction = MobDirection.West;
                newX -= walkDistance;

                if (newX <= path.FirstNode.Location.X)
                {
                    newX = path.FirstNode.Location.X;
                    path.RemoveAt(0);
                }
            }
            else if (path.FirstNode.Location.Y > player.Location.Y)
            {
                player.Direction = MobDirection.South;
                newY += walkDistance;

                if (newY >= path.FirstNode.Location.Y)
                {
                    newY = path.FirstNode.Location.Y;
                    path.RemoveAt(0);
                }
            }
            else if (path.FirstNode.Location.X > player.Location.X)
            {
                player.Direction = MobDirection.East;
                newX += walkDistance;

                if (newX >= path.FirstNode.Location.X)
                {
                    newX = path.FirstNode.Location.X;
                    path.RemoveAt(0);
                }
            }

            player.Location = new PointF2D(newX, newY);
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
