using NuciXNA.Primitives;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IGameManager
    {
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns>The player.</returns>
        Mob GetPlayer();
        
        /// <summary>
        /// Moves the player to the specified location.
        /// </summary>
        /// <param name="location">Location.</param>
        void MovePlayer(Point2D location);
    }
}
