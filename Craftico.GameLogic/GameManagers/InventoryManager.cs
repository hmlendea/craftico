using System;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public class InventoryManager : IInventoryManager
    {
        readonly Mob player;

        readonly IEntityManager entityManager;

        public InventoryManager(
            Mob player,
            IEntityManager entityManager)
        {
            this.player = player;
            this.entityManager = entityManager;
        }

        public bool IsItemEquipped(string itemId)
        {
            throw new NotImplementedException();
        }
        
        public InventorySlot GetInventorySlot(int slot)
        {
            if (player.Inventory.Size < slot)
            {
                return null;
            }

            return player.Inventory[slot];
        }
        
        public void SetInventorySlotQuantity(int slot, int quantity)
        {
            player.Inventory[slot].Quantity = quantity;
        }
        
        public void RemoveItem(int slotIndex)
        {
            InventorySlot slot = player.Inventory[slotIndex];

            slot.ItemId = null;
            slot.Quantity = 0;
        }

        public int GetItemTotalCount(string itemId)
        {
            int count = 0;

            for (int i = 0; i < player.Inventory.Size; i++)
            {
                InventorySlot slot = player.Inventory[i];

                if (slot.ItemId == itemId)
                {
                    count += slot.Quantity;
                }
            }

            return count;
        }
    }
}
