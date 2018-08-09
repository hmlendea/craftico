using System;
using System.Linq;

using Microsoft.Xna.Framework;

using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public class InventoryManager : IInventoryManager
    {
        readonly IEntityManager entities;

        Mob player;

        public InventoryManager(
            IEntityManager entities)
        {
            this.entities = entities;
        }

        public void LoadContent()
        {
            player = entities.GetPlayer();
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void AddItem(string itemId)
        {
            AddItem(itemId, 1);
        }

        public void AddItem(string itemId, int quantity)
        {
            if (player.Inventory.FreeSlotsCount == 0)
            {
                // TODO: Handle this
                throw new Exception();
            }

            int freeSlotIndex = player.Inventory.InventorySlots.TakeWhile(x => x != null && !x.IsEmpty).Count();

            player.Inventory.InventorySlots[freeSlotIndex] = new InventorySlot
            {
                ItemId = itemId,
                Quantity = quantity
            };
        }

        public void RemoveItem(string itemId)
        {
            RemoveItem(itemId, 1);
        }

        public void RemoveItem(string itemId, int quantity)
        {
            int quantityInInventory = player.Inventory.InventorySlots.Where(x => x != null && x.ItemId == itemId).Sum(x => x.Quantity);

            if (quantityInInventory < quantity)
            {
                // TODO: Handle this properly
                throw new Exception();
            }

            int quantityLeftToRemove = quantity;

            for (int i = 0; i < player.Inventory.Size && quantityLeftToRemove > 0; i++)
            {
                InventorySlot slot = player.Inventory.InventorySlots[i];

                if (slot == null || slot.IsEmpty || slot.ItemId != itemId)
                {
                    continue;
                }

                if (slot.Quantity <= quantityLeftToRemove)
                {
                    quantityLeftToRemove -= slot.Quantity;
                    slot.Empty();
                }
                else
                {
                    slot.Quantity -= quantityLeftToRemove;
                    quantityLeftToRemove = 0;
                }
            }
        }

        public void Unequip(InventorySlot equipmentSlot)
        {
            if (equipmentSlot == null || equipmentSlot.IsEmpty || player.Inventory.FreeSlotsCount == 0)
            {
                return;
            }

            AddItem(equipmentSlot.ItemId, equipmentSlot.Quantity);
            equipmentSlot.Empty();
        }

        public void Equip(int slotIndex)
        {
            InventorySlot slot = player.Inventory.InventorySlots[slotIndex];

            if (slot == null || slot.IsEmpty)
            {
                // TODO: Handle this properly
                throw new Exception();
            }

            Item item = entities.GetItem(slot.ItemId);

            if (!item.IsEquipable)
            {
                // TODO: Handle this properly
                throw new Exception();
            }

            string itemId = slot.ItemId;
            int quantity = slot.Quantity;

            RemoveItem(slot.ItemId, slot.Quantity);

            InventorySlot equipmentSlot = GetInventorySlotByItemType(item.Type);

            if (!equipmentSlot.IsEmpty)
            {
                Unequip(equipmentSlot);
            }

            equipmentSlot.ItemId = itemId;
            equipmentSlot.Quantity = quantity;
        }

        InventorySlot GetInventorySlotByItemType(ItemType type)
        {
            if (type == ItemType.Helmet)
            {
                return player.Inventory.HelmetSlot;
            }

            if (type == ItemType.Cuirass)
            {
                return player.Inventory.CuirassSlot;
            }

            if (type == ItemType.Greaves)
            {
                return player.Inventory.GreavesSlot;
            }

            if (type == ItemType.Boots)
            {
                return player.Inventory.BootsSlot;
            }

            if (type == ItemType.Gloves)
            {
                return player.Inventory.GlovesSlot;
            }

            if (type == ItemType.Weapon)
            {
                return player.Inventory.LeftHandSlot;
            }

            if (type == ItemType.Shield)
            {
                return player.Inventory.RightHandSlot;
            }

            if (type == ItemType.Ammo)
            {
                return player.Inventory.AmmoSlot;
            }

            return null;
        }
    }
}
