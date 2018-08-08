using NuciXNA.Gui.GuiElements;

namespace Craftico.Gui.GuiElements
{
    public class GuiSideBarPanel : GuiElement
    {
        GuiImage background;

        public override void LoadContent()
        {
            background = new GuiImage
            {
                ContentFile = "Interface/SideBar/panel"
            };

            AddChild(background);

            base.LoadContent();
        }
    }
}
