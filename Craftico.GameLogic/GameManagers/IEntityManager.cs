using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IEntityManager
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="id">Identifier.</param>
        Item GetItem(string id);

        /// <summary>
        /// Gets the mob.
        /// </summary>
        /// <returns>The mob.</returns>
        /// <param name="id">Identifier.</param>
        Mob GetMob(string id);
    }
}
