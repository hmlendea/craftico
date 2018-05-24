using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IInventoryManager
    {
        bool IsItemEquipped(string itemId);
        
        InventorySlot GetInventorySlot(int slot);
        
        void SetInventorySlotQuantity(int slot, int quantity);
        
        void RemoveItem(int slot);

        int GetItemTotalCount(string itemId);
    }
}
