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

        readonly IEntityManager entityManager;
        readonly IInventoryManager inventoryManager;
        readonly IWorldManager worldManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            player = new Mob
            {
                Inventory = new Inventory(28)
                {
                    HelmetSlot = new InventorySlot
                    {
                        ItemId = "leather_cap",
                        Quantity = 1
                    },
                    CuirassSlot = new InventorySlot
                    {
                        ItemId = "leather_chest",
                        Quantity = 1
                    }
                }
            };

            entityManager = new EntityManager();
            inventoryManager = new InventoryManager(player, entityManager);
            worldManager = new WorldManager();
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns>The player.</returns>
        public Mob GetPlayer()
        => player;

        /// <summary>
        /// Moves the player to the specified location.
        /// </summary>
        /// <param name="location">Location.</param>
        public void MovePlayer(Point2D location)
        {
            // TODO: Actual movement logic
            player.Location = location;
        }
    }
}
