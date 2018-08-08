using Microsoft.Xna.Framework;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiEquipmentPanel : GuiElement
    {
        readonly IEntityManager entities;
        readonly IGameManager game;
        readonly Mob player;

        GuiInventorySlot helmetSlot;
        GuiInventorySlot cuirassSlot;
        GuiInventorySlot greavesSlot;
        GuiInventorySlot glovesSlot;
        GuiInventorySlot bootsSlot;
        GuiInventorySlot leftHandSlot;
        GuiInventorySlot rightHandSlot;
        GuiInventorySlot ammoSlot;

        const int Rows = 4;
        const int Columns = 3;

        public GuiEquipmentPanel(
            IEntityManager entities,
            IGameManager game)
        {
            this.entities = entities;
            this.game = game;

            player = game.GetPlayer();
        }

        public override void LoadContent()
        {
            helmetSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-helmet" };
            cuirassSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-cuirass" };
            greavesSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-greaves" };
            glovesSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-gloves" };
            bootsSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-boots" };
            leftHandSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-weapon" };
            rightHandSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-shield" };
            ammoSlot = new GuiInventorySlot { PlaceholderIcon = "placeholder-ammo" };

            AddChild(helmetSlot);
            AddChild(cuirassSlot);
            AddChild(greavesSlot);
            AddChild(glovesSlot);
            AddChild(bootsSlot);
            AddChild(leftHandSlot);
            AddChild(rightHandSlot);
            AddChild(ammoSlot);

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

            int slotSize = helmetSlot.Size.Width;

            int spacingX = (Size.Width - Columns * slotSize) / (Columns + 1);
            int spacingY = (Size.Height - Rows * slotSize) / (Rows + 1);

            helmetSlot.Location = new Point2D((Size.Width - slotSize) / 2, spacingY);
            cuirassSlot.Location = new Point2D(helmetSlot.ClientRectangle.Left, helmetSlot.ClientRectangle.Bottom + spacingY);
            greavesSlot.Location = new Point2D(cuirassSlot.ClientRectangle.Left, cuirassSlot.ClientRectangle.Bottom + spacingY);
            bootsSlot.Location = new Point2D(greavesSlot.ClientRectangle.Left, greavesSlot.ClientRectangle.Bottom + spacingY);
            glovesSlot.Location = new Point2D(bootsSlot.ClientRectangle.Left - spacingX - slotSize, bootsSlot.ClientRectangle.Top);
            leftHandSlot.Location = new Point2D(cuirassSlot.ClientRectangle.Left - spacingX - slotSize, cuirassSlot.ClientRectangle.Top);
            rightHandSlot.Location = new Point2D(cuirassSlot.ClientRectangle.Right + spacingX, cuirassSlot.ClientRectangle.Top);
            ammoSlot.Location = new Point2D(helmetSlot.ClientRectangle.Right + spacingX, helmetSlot.ClientRectangle.Top);
        }

        void SetItems()
        {
            SetItem(helmetSlot, player.Inventory.HelmetSlot);
            SetItem(cuirassSlot, player.Inventory.CuirassSlot);
            SetItem(greavesSlot, player.Inventory.GreavesSlot);
            SetItem(glovesSlot, player.Inventory.GlovesSlot);
            SetItem(leftHandSlot, player.Inventory.LeftHandSlot);
            SetItem(rightHandSlot, player.Inventory.RightHandSlot);
            SetItem(ammoSlot, player.Inventory.AmmoSlot);
        }

        void SetItem(GuiInventorySlot guiSlot, InventorySlot slot)
        {
            if (slot == null || slot.IsEmpty)
            {
                return;
            }

            Item item = entities.GetItem(slot.ItemId);

            guiSlot.ItemIcon = item.SpriteSheet;
            guiSlot.Quantity = slot.Quantity;
        }
    }
}
