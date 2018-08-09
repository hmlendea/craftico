using Microsoft.Xna.Framework;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IInventoryManager
    {
        void LoadContent();

        void UnloadContent();

        void Update(GameTime gameTime);

        void AddItem(string itemId);

        void AddItem(string itemId, int quantity);

        void RemoveItem(string itemId);

        void RemoveItem(string itemId, int quantity);

        void Unequip(InventorySlot equipmentSlot);

        void Equip(int slotIndex);
    }
}
