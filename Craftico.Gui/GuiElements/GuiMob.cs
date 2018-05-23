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
        Sprite bodyAppearance;
        Sprite headEquipment;
        Sprite handEquipment;
        Sprite torsoEquipment;

        HumanSpriteSheetEffect bodyAppearanceEffect;
        HumanSpriteSheetEffect headEquipmentEffect;
        HumanSpriteSheetEffect handEquipmentEffect;
        HumanSpriteSheetEffect torsoEquipmentEffect;

        public MobAction Action { get; set; }

        public MobDirection Direction { get; set; }

        public override void LoadContent()
        {
            bodyAppearanceEffect = new HumanSpriteSheetEffect();
            headEquipmentEffect = new HumanSpriteSheetEffect();
            handEquipmentEffect = new HumanSpriteSheetEffect();
            torsoEquipmentEffect = new HumanSpriteSheetEffect();

            bodyAppearance = new Sprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SpriteSheetEffect = bodyAppearanceEffect,
                Active = true
            };
            headEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/leather_cap_male",
                SpriteSheetEffect = headEquipmentEffect,
                Active = true
            };
            handEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/metal_gloves_male",
                SpriteSheetEffect = handEquipmentEffect,
                Active = true
            };
            torsoEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/leather_chest_male",
                SpriteSheetEffect = torsoEquipmentEffect,
                Active = true
            };

            bodyAppearance.SpriteSheetEffect.AssociateSprite(bodyAppearance);
            headEquipment.SpriteSheetEffect.AssociateSprite(headEquipment);
            handEquipment.SpriteSheetEffect.AssociateSprite(handEquipment);
            torsoEquipment.SpriteSheetEffect.AssociateSprite(torsoEquipment);

            bodyAppearance.LoadContent();
            headEquipment.LoadContent();
            handEquipment.LoadContent();
            torsoEquipment.LoadContent();

            base.LoadContent();

            bodyAppearance.SpriteSheetEffect.Activate();
            headEquipment.SpriteSheetEffect.Activate();
            handEquipment.SpriteSheetEffect.Activate();
            torsoEquipment.SpriteSheetEffect.Activate();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            bodyAppearance.UnloadContent();
            headEquipment.UnloadContent();
            handEquipment.UnloadContent();
            torsoEquipment.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bodyAppearance.Update(gameTime);
            headEquipment.Update(gameTime);
            handEquipment.Update(gameTime);
            torsoEquipment.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            bodyAppearance.Draw(spriteBatch);
            headEquipment.Draw(spriteBatch);
            handEquipment.Draw(spriteBatch);
            torsoEquipment.Draw(spriteBatch);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            bodyAppearanceEffect.Action = Action;
            headEquipmentEffect.Action = Action;
            handEquipmentEffect.Action = Action;
            torsoEquipmentEffect.Action = Action;

            bodyAppearanceEffect.Direction = Direction;
            headEquipmentEffect.Direction = Direction;
            handEquipmentEffect.Direction = Direction;
            torsoEquipmentEffect.Direction = Direction;

            bodyAppearance.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - bodyAppearance.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - bodyAppearance.SourceRectangle.Height);
            headEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - headEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - headEquipment.SourceRectangle.Height);
            handEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - handEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - handEquipment.SourceRectangle.Height);
            torsoEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - torsoEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - torsoEquipment.SourceRectangle.Height);
        }
    }
}