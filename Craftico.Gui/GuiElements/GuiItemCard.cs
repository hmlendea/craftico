using Microsoft.Xna.Framework;

using NuciXNA.Graphics.Drawing;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

namespace Craftico.Gui.GuiElements
{
    public class GuiItemCard : GuiElement
    {
        const int SpriteRows = 32;
        const int SpriteColumns = 32;

        GuiImage icon;
        GuiText quantity;

        public string ItemIcon { get; set; }

        public int Quantity { get; set; }

        public GuiItemCard()
        {
            ItemIcon = "none";
            ForegroundColour = Colour.Gold;
            Size = new Size2D(36, 36);
        }

        public override void LoadContent()
        {
            icon = new GuiImage
            {
                Size = new Size2D(32, 32)
            };
            quantity = new GuiText
            {
                Size = new Size2D(Size.Width, 10),
                FontName = "ItemCardFont",
                FontOutline = FontOutline.BottomRight,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            AddChild(icon);
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
            icon.SourceRectangle = new Rectangle2D(Point2D.Empty, icon.Size);
            icon.Location = new Point2D(
                (Size.Width - icon.Size.Width) / 2,
                (Size.Height - icon.Size.Height) / 2);

            quantity.Text = Quantity.ToString();
        }
    }
}
