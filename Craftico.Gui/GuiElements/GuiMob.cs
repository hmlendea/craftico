using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NuciXNA.Graphics;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.Gui.MobAnimationEffects;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiMob : GuiElement
    {
        Sprite mob;
        HumanSpriteSheetEffect humanEffect;

        public MobAction Action { get; set; }

        public MobDirection Direction { get; set; }

        public override void LoadContent()
        {
            humanEffect = new HumanSpriteSheetEffect();
            mob = new Sprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SpriteSheetEffect = humanEffect,
                Active = true
            };

            mob.SpriteSheetEffect.AssociateSprite(mob);
            mob.LoadContent();

            base.LoadContent();

            mob.SpriteSheetEffect.Activate();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            mob.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            mob.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            mob.Draw(spriteBatch);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            humanEffect.Action = Action;
            humanEffect.Direction = Direction;

            if (mob.SourceRectangle.Width == GameDefines.MAP_TILE_SIZE &&
                mob.SourceRectangle.Height == GameDefines.MAP_TILE_SIZE)
            {
                mob.Location = Location;
            }
            else
            {
                mob.Location = new Point2D(
                    Location.X + (GameDefines.MAP_TILE_SIZE - mob.SourceRectangle.Width) / 2,
                    Location.Y + GameDefines.MAP_TILE_SIZE - mob.SourceRectangle.Height);
            }
        }
    }
}