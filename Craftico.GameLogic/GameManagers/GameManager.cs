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

        readonly IWorldManager worldManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager() : this(new WorldManager())
        {
            player = new Mob();
        }

        public GameManager(IWorldManager worldManager)
        {
            this.worldManager = worldManager;
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
