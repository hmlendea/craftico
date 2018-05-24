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
        Sprite handsEquipment;
        Sprite torsoEquipment;
        Sprite armsEquipment;
        Sprite shouldersEquipment;
        Sprite legsEquipment;

        HumanSpriteSheetEffect bodyAppearanceEffect;
        HumanSpriteSheetEffect headEquipmentEffect;
        HumanSpriteSheetEffect handEquipmentEffect;
        HumanSpriteSheetEffect torsoEquipmentEffect;
        HumanSpriteSheetEffect armsEquipmentEffect;
        HumanSpriteSheetEffect shouldersEquipmentEffect;
        HumanSpriteSheetEffect legsEquipmentEffect;

        public MobAction Action { get; set; }

        public MobDirection Direction { get; set; }

        public override void LoadContent()
        {
            bodyAppearanceEffect = new HumanSpriteSheetEffect();
            headEquipmentEffect = new HumanSpriteSheetEffect();
            handEquipmentEffect = new HumanSpriteSheetEffect();
            torsoEquipmentEffect = new HumanSpriteSheetEffect();
            armsEquipmentEffect = new HumanSpriteSheetEffect();
            shouldersEquipmentEffect = new HumanSpriteSheetEffect();
            legsEquipmentEffect = new HumanSpriteSheetEffect();

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
            handsEquipment = new Sprite
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
            armsEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/plate_arms_male",
                SpriteSheetEffect = armsEquipmentEffect,
                Active = true
            };
            shouldersEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/leather_shoulders_male",
                SpriteSheetEffect = shouldersEquipmentEffect,
                Active = true
            };
            legsEquipment = new Sprite
            {
                ContentFile = "SpriteSheets/Equipment/metal_greaves_male",
                SpriteSheetEffect = legsEquipmentEffect,
                Active = true
            };

            bodyAppearance.SpriteSheetEffect.AssociateSprite(bodyAppearance);
            headEquipment.SpriteSheetEffect.AssociateSprite(headEquipment);
            handsEquipment.SpriteSheetEffect.AssociateSprite(handsEquipment);
            torsoEquipment.SpriteSheetEffect.AssociateSprite(torsoEquipment);
            armsEquipment.SpriteSheetEffect.AssociateSprite(armsEquipment);
            shouldersEquipment.SpriteSheetEffect.AssociateSprite(shouldersEquipment);
            legsEquipment.SpriteSheetEffect.AssociateSprite(legsEquipment);

            bodyAppearance.LoadContent();
            headEquipment.LoadContent();
            handsEquipment.LoadContent();
            torsoEquipment.LoadContent();
            armsEquipment.LoadContent();
            shouldersEquipment.LoadContent();
            legsEquipment.LoadContent();

            base.LoadContent();

            bodyAppearance.SpriteSheetEffect.Activate();
            headEquipment.SpriteSheetEffect.Activate();
            handsEquipment.SpriteSheetEffect.Activate();
            torsoEquipment.SpriteSheetEffect.Activate();
            armsEquipment.SpriteSheetEffect.Activate();
            shouldersEquipment.SpriteSheetEffect.Activate();
            legsEquipment.SpriteSheetEffect.Activate();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            bodyAppearance.UnloadContent();
            headEquipment.UnloadContent();
            handsEquipment.UnloadContent();
            torsoEquipment.UnloadContent();
            armsEquipment.UnloadContent();
            shouldersEquipment.UnloadContent();
            legsEquipment.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bodyAppearance.Update(gameTime);
            headEquipment.Update(gameTime);
            handsEquipment.Update(gameTime);
            torsoEquipment.Update(gameTime);
            armsEquipment.Update(gameTime);
            shouldersEquipment.Update(gameTime);
            legsEquipment.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            bodyAppearance.Draw(spriteBatch);
            headEquipment.Draw(spriteBatch);
            handsEquipment.Draw(spriteBatch);
            torsoEquipment.Draw(spriteBatch);
            armsEquipment.Draw(spriteBatch);
            shouldersEquipment.Draw(spriteBatch);
            legsEquipment.Draw(spriteBatch);
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            bodyAppearanceEffect.Action = Action;
            headEquipmentEffect.Action = Action;
            handEquipmentEffect.Action = Action;
            torsoEquipmentEffect.Action = Action;
            armsEquipmentEffect.Action = Action;
            shouldersEquipmentEffect.Action = Action;
            legsEquipmentEffect.Action = Action;

            bodyAppearanceEffect.Direction = Direction;
            headEquipmentEffect.Direction = Direction;
            handEquipmentEffect.Direction = Direction;
            torsoEquipmentEffect.Direction = Direction;
            armsEquipmentEffect.Direction = Direction;
            shouldersEquipmentEffect.Direction = Direction;
            legsEquipmentEffect.Direction = Direction;

            bodyAppearance.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - bodyAppearance.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - bodyAppearance.SourceRectangle.Height);
            headEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - headEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - headEquipment.SourceRectangle.Height);
            handsEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - handsEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - handsEquipment.SourceRectangle.Height);
            torsoEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - torsoEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - torsoEquipment.SourceRectangle.Height);
            armsEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - armsEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - armsEquipment.SourceRectangle.Height);
            shouldersEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - shouldersEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - shouldersEquipment.SourceRectangle.Height);
            legsEquipment.Location = new Point2D(
                Location.X + (GameDefines.MAP_TILE_SIZE - legsEquipment.SourceRectangle.Width) / 2,
                Location.Y + GameDefines.MAP_TILE_SIZE - legsEquipment.SourceRectangle.Height);
        }
    }
}