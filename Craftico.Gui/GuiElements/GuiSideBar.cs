using NuciXNA.Gui.GuiElements;
using NuciXNA.Input;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiSideBar : GuiElement
    {
        IGameManager game;

        GuiSideBarPanel panel;
        GuiSkillsPanel skillsPanel;

        GuiToggleButton skillsButton;
        GuiToggleButton exitButton;

        public override void LoadContent()
        {
            panel = new GuiSideBarPanel { Size = new Size2D(240, 262) };
            skillsPanel = new GuiSkillsPanel { Size = new Size2D(190, 262) };

            skillsButton = new GuiToggleButton
            {
                ContentFile = "Interface/SideBar/button",
                Icon = "Interface/SideBar/skills_button_icon",
                Size = new Size2D(30, 32)
            };
            exitButton = new GuiToggleButton
            {
                ContentFile = "Interface/SideBar/button-full",
                Icon = "Interface/SideBar/exit_button_icon",
                Size = new Size2D(240, 32)
            };

            AddChild(panel);
            AddChild(skillsPanel);

            AddChild(skillsButton);
            AddChild(exitButton);

            SkillsButton_Clicked(this, null);

            base.LoadContent();
        }

        public void AssociateGameManager(IGameManager game)
        {
            this.game = game;

            skillsPanel.AssociateGameManager(game);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            exitButton.Location = new Point2D(0, Size.Height - GameDefines.GUI_TILE_SIZE);
            panel.Location = new Point2D(0, GameDefines.GUI_TILE_SIZE);

            skillsPanel.Location = new Point2D(
                panel.Location.X + 25,
                panel.Location.Y);

            skillsButton.Location = new Point2D(
                panel.Location.X,
                panel.Location.Y - skillsButton.Size.Height);
        }

        protected override void RegisterEvents()
        {
            skillsButton.Clicked += SkillsButton_Clicked;
            exitButton.Clicked += ExitButton_Clicked;
        }

        protected override void UnregisterEvents()
        {
            skillsButton.Clicked -= SkillsButton_Clicked;
            exitButton.Clicked -= ExitButton_Clicked;
        }

        void CombatButton_Clicked(object sender, MouseButtonEventArgs e)
        {
            UnselectEverything();

            skillsButton.Toggled = true;
            skillsPanel.Show();
        }

        void SkillsButton_Clicked(object sender, MouseButtonEventArgs e)
        {
            UnselectEverything();

            skillsButton.Toggled = true;
            skillsPanel.Show();
        }

        void ExitButton_Clicked(object sender, MouseButtonEventArgs e)
        {
            UnselectEverything();

            exitButton.Toggled = true;
        }

        void UnselectEverything()
        {
            skillsButton.Toggled = false;
            exitButton.Toggled = false;

            skillsPanel.Hide();
        }
    }
}
