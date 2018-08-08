using Microsoft.Xna.Framework;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiSideBarInventoryPanel : GuiElement
    {
        readonly IEntityManager entities;
        readonly IGameManager game;
        readonly Mob player;

        GuiInventorySlot[] itemCards;

        const int Rows = 7;
        const int Columns = 4;

        public GuiSideBarInventoryPanel(
            IEntityManager entities,
            IGameManager game)
        {
            this.entities = entities;
            this.game = game;

            player = game.GetPlayer();
        }

        public override void LoadContent()
        {
            itemCards = new GuiInventorySlot[Rows * Columns];

            for (int i = 0; i < Rows * Columns; i++)
            {
                itemCards[i] = new GuiInventorySlot();

                AddChild(itemCards[i]);
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

            int spacingX = (Size.Width - Columns * itemCards[0].Size.Width) / (Columns + 1);
            int spacingY = (Size.Height - Rows * itemCards[0].Size.Height) / (Rows + 1);

            for (int i = 0; i < Rows * Columns; i++)
            {
                int x = i % Columns;
                int y = i / Columns;

                itemCards[i].Location = new Point2D(
                    spacingX * (x + 1) + itemCards[i].Size.Width * x,
                    spacingY * (y + 1) + itemCards[i].Size.Height * y);
            }
        }

        void SetItems()
        {
            for (int itemSlot = 0; itemSlot < GameDefines.InventorySize; itemSlot++)
            {
                InventorySlot slot = player.Inventory[itemSlot];

                if (slot == null || slot.IsEmpty)
                {
                    continue;
                }

                Item item = entities.GetItem(slot.ItemId);

                itemCards[itemSlot].ItemIcon = item.SpriteSheet;
                itemCards[itemSlot].Quantity = slot.Quantity;
            }
        }
    }
}
