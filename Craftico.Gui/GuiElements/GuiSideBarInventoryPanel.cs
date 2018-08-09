using System;

using Microsoft.Xna.Framework;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiSideBarInventoryPanel : GuiElement
    {
        readonly IEntityManager entities;
        readonly IInventoryManager inventory;
        readonly IGameManager game;

        readonly Mob player;

        GuiInventorySlot[] slots;

        const int Rows = 7;
        const int Columns = 4;

        public GuiSideBarInventoryPanel(
            IEntityManager entities,
            IInventoryManager inventory,
            IGameManager game)
        {
            this.entities = entities;
            this.inventory = inventory;
            this.game = game;

            player = entities.GetPlayer();
        }

        public override void LoadContent()
        {
            slots = new GuiInventorySlot[Rows * Columns];

            for (int i = 0; i < Rows * Columns; i++)
            {
                slots[i] = new GuiInventorySlot
                {
                    SlotIndex = i
                };

                AddChild(slots[i]);
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            SetItems();
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            int spacingX = (Size.Width - Columns * slots[0].Size.Width) / (Columns + 1);
            int spacingY = (Size.Height - Rows * slots[0].Size.Height) / (Rows + 1);

            for (int i = 0; i < Rows * Columns; i++)
            {
                int x = i % Columns;
                int y = i / Columns;

                slots[i].Location = new Point2D(
                    spacingX * (x + 1) + slots[i].Size.Width * x,
                    spacingY * (y + 1) + slots[i].Size.Height * y);
            }
        }

        protected override void RegisterEvents()
        {
            base.RegisterEvents();

            foreach (GuiInventorySlot slot in slots)
            {
                slot.Clicked += Slot_Clicked;
            }
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();

            foreach (GuiInventorySlot slot in slots)
            {
                slot.Clicked -= Slot_Clicked;
            }
        }

        void SetItems()
        {
            for (int itemSlot = 0; itemSlot < GameDefines.InventorySize; itemSlot++)
            {
                InventorySlot slot = player.Inventory[itemSlot];

                if (slot == null || slot.IsEmpty)
                {
                    slots[itemSlot].ItemIcon = GuiInventorySlot.BlankIcon;
                    slots[itemSlot].Quantity = 0;

                    continue;
                }

                Item item = entities.GetItem(slot.ItemId);

                slots[itemSlot].ItemIcon = item.SpriteSheet;
                slots[itemSlot].Quantity = slot.Quantity;
            }
        }

        void Slot_Clicked(object sender, MouseButtonEventArgs e)
        {
            GuiInventorySlot clickedSlot = null;

            // TODO: Don't search for it every time - fix the sender object in NuciXNA
            foreach (GuiInventorySlot slot in slots)
            {
                if (slot.DisplayRectangle.Contains(e.Location))
                {
                    clickedSlot = slot;
                    break;
                }
            }

            if (clickedSlot == null)
            {
                // TODO: Handle this exception properly
                throw new Exception();
            }

            inventory.Equip(clickedSlot.SlotIndex);
        }
    }
}
