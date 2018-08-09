using Microsoft.Xna.Framework;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;

namespace Craftico.Gui.GuiElements
{
    public class GuiEquipmentPanel : GuiElement
    {
        readonly IEntityManager entities;
        readonly IInventoryManager inventory;
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

        protected override void RegisterEvents()
        {
            base.RegisterEvents();

            helmetSlot.Clicked += HelmetSlot_Clicked;
            cuirassSlot.Clicked += CuirassSlot_Clicked;
            greavesSlot.Clicked += GreavesSlot_Clicked;
            bootsSlot.Clicked += BootsSlot_Clicked;
            glovesSlot.Clicked += GlovesSlot_Clicked;
            leftHandSlot.Clicked += LeftHandSlot_Clicked;
            rightHandSlot.Clicked += RightHandSlot_Clicked;
            ammoSlot.Clicked += AmmoSlot_Clicked;
        }

        protected override void UnregisterEvents()
        {
            base.UnregisterEvents();

            helmetSlot.Clicked -= HelmetSlot_Clicked;
            cuirassSlot.Clicked -= CuirassSlot_Clicked;
            greavesSlot.Clicked -= GreavesSlot_Clicked;
            bootsSlot.Clicked -= BootsSlot_Clicked;
            glovesSlot.Clicked -= GlovesSlot_Clicked;
            leftHandSlot.Clicked -= LeftHandSlot_Clicked;
            rightHandSlot.Clicked -= RightHandSlot_Clicked;
            ammoSlot.Clicked -= AmmoSlot_Clicked;
        }

        void SetItems()
        {
            SetItem(helmetSlot, player.Inventory.HelmetSlot);
            SetItem(cuirassSlot, player.Inventory.CuirassSlot);
            SetItem(greavesSlot, player.Inventory.GreavesSlot);
            SetItem(glovesSlot, player.Inventory.GlovesSlot);
            SetItem(bootsSlot, player.Inventory.BootsSlot);
            SetItem(leftHandSlot, player.Inventory.LeftHandSlot);
            SetItem(rightHandSlot, player.Inventory.RightHandSlot);
            SetItem(ammoSlot, player.Inventory.AmmoSlot);
        }

        void SetItem(GuiInventorySlot guiSlot, InventorySlot slot)
        {
            if (slot == null || slot.IsEmpty)
            {
                guiSlot.ItemIcon = GuiInventorySlot.BlankIcon;
                return;
            }

            Item item = entities.GetItem(slot.ItemId);

            guiSlot.ItemIcon = item.SpriteSheet;
            guiSlot.Quantity = slot.Quantity;
        }

        void HelmetSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.HelmetSlot);
        }

        void CuirassSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.CuirassSlot);
        }

        void GreavesSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.GreavesSlot);
        }

        void BootsSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.BootsSlot);
        }

        void GlovesSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.GlovesSlot);
        }

        void LeftHandSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.LeftHandSlot);
        }

        void RightHandSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.RightHandSlot);
        }

        void AmmoSlot_Clicked(object sender, MouseButtonEventArgs e)
        {
            inventory.Unequip(player.Inventory.AmmoSlot);
        }
    }
}
