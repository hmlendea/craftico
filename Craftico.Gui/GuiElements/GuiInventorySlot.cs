using Microsoft.Xna.Framework;

using NuciXNA.Graphics.Drawing;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

namespace Craftico.Gui.GuiElements
{
    public class GuiInventorySlot : GuiElement
    {
        const int SpriteRows = 32;
        const int SpriteColumns = 32;

        const string BlankIcon = "none";
        const string BlankPlaceholder = "placeholder-blank";

        GuiImage background;
        GuiImage icon;
        GuiImage placeholderIcon;
        GuiText quantity;

        public string PlaceholderIcon { get; set; }

        public string ItemIcon { get; set; }

        public int Quantity { get; set; }

        public GuiInventorySlot()
        {
            ItemIcon = BlankIcon;
            PlaceholderIcon = BlankPlaceholder;

            ForegroundColour = Colour.Gold;
            Size = new Size2D(36, 36);
        }

        public override void LoadContent()
        {
            background = new GuiImage
            {
                ContentFile = "Interface/Inventory/slot"
            };
            icon = new GuiImage
            {
                Size = new Size2D(32, 32)
            };
            placeholderIcon = new GuiImage
            {
                Size = icon.Size
            };
            quantity = new GuiText
            {
                Location = new Point2D(2, 2),
                Size = new Size2D(Size.Width - 2, 10),
                FontName = "ItemCardFont",
                FontOutline = FontOutline.BottomRight,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            AddChild(background);
            AddChild(icon);
            AddChild(placeholderIcon);
            AddChild(quantity);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            icon.Visible = Quantity > 0;
            quantity.Visible = Quantity > 1;

            base.Update(gameTime);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            icon.ContentFile = $"Icons/Items/{ItemIcon}";
            icon.Location = new Point2D(
                (Size.Width - icon.Size.Width) / 2,
                (Size.Height - icon.Size.Height) / 2);

            placeholderIcon.ContentFile = $"Interface/Inventory/{PlaceholderIcon}";
            placeholderIcon.Location = icon.Location;

            quantity.Text = Quantity.ToString();

            if (ItemIcon != BlankIcon)
            {
                icon.Show();
                placeholderIcon.Hide();
            }
            else if (ItemIcon != PlaceholderIcon)
            {
                icon.Hide();
                placeholderIcon.Show();
            }
            else
            {
                icon.Hide();
                placeholderIcon.Hide();
            }
        }
    }
}
