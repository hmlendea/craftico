using Microsoft.Xna.Framework;
using NuciXNA.Primitives;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IGameManager
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Unloads the content.
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        void Update(GameTime gameTime);

        void MovePlayer(Point2D destination);
    }
}
