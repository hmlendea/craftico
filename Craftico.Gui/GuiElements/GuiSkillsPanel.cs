using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;

namespace Craftico.Gui.GuiElements
{
    public class GuiSkillsPanel : GuiElement
    {
        IGameManager game;
        
        GuiSkillCard hitpointsCard;
        GuiSkillCard faithCard;

        public override void LoadContent()
        {
            hitpointsCard = new GuiSkillCard { SkillIcon = "Icons/Skills/vigour" };
            faithCard = new GuiSkillCard { SkillIcon = "Icons/Skills/faith" };
            
            AddChild(hitpointsCard);
            AddChild(faithCard);

            base.LoadContent();
        }

        public void AssociateGameManager(IGameManager game)
        {
            this.game = game;
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            int spacingX = 1;
            int spacingY = 1;

            // TODO: Ugly fix
            if (game == null)
            {
                return;
            }

            Mob player = game.GetPlayer();

            hitpointsCard.CurrentLevel = player.Vigour.CurrentLevel;
            hitpointsCard.BaseLevel = player.Vigour.BaseLevel;
            hitpointsCard.Experience = player.Vigour.Experience;
            hitpointsCard.Location = new Point2D(
                Location.X + (Size.Width - 3 * hitpointsCard.Size.Width - 2 * spacingX) / 2,
                Location.Y + (Size.Width - 3 * hitpointsCard.Size.Width - 2 * spacingX) / 2);

            faithCard.CurrentLevel = player.Faith.CurrentLevel;
            faithCard.BaseLevel = player.Faith.BaseLevel;
            faithCard.Experience = player.Faith.Experience;
            faithCard.Location = new Point2D(
                hitpointsCard.ClientRectangle.Right + spacingX,
                hitpointsCard.ClientRectangle.Top);
        }
    }
}
