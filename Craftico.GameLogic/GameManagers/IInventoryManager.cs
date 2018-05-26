using Microsoft.Xna.Framework;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IInventoryManager
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

        bool IsItemEquipped(string itemId);

        InventorySlot GetInventorySlot(int slot);

        void SetInventorySlotQuantity(int slot, int quantity);

        void RemoveItem(int slot);

        int GetItemTotalCount(string itemId);
    }
}
