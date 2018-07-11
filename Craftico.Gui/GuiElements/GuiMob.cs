using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NuciXNA.Graphics.Drawing;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.Gui.MobAnimationEffects;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiMob : GuiElement
    {
        Mob mob;

        TextureSprite body;
        TextureSprite helmet;
        TextureSprite cuirass;
        TextureSprite greaves;
        TextureSprite gloves;
        TextureSprite boots;
        TextureSprite leftHand;
        TextureSprite rightHand;

        HumanSpriteSheetEffect bodyEffect;
        HumanSpriteSheetEffect helmetEffect;
        HumanSpriteSheetEffect cuirassEffect;
        HumanSpriteSheetEffect greavesEffect;
        HumanSpriteSheetEffect glovesEffect;
        HumanSpriteSheetEffect bootsEffect;
        HumanSpriteSheetEffect leftHandEffect;
        HumanSpriteSheetEffect rightHandEffect;

        public GuiMob()
        {
            Size = new Size2D(64, 64);
        }

        public override void LoadContent()
        {
            bodyEffect = new HumanSpriteSheetEffect();
            helmetEffect = new HumanSpriteSheetEffect();
            cuirassEffect = new HumanSpriteSheetEffect();
            greavesEffect = new HumanSpriteSheetEffect();
            glovesEffect = new HumanSpriteSheetEffect();
            bootsEffect = new HumanSpriteSheetEffect();
            leftHandEffect = new HumanSpriteSheetEffect();
            rightHandEffect = new HumanSpriteSheetEffect();

            body = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = bodyEffect,
                Active = true
            };
            helmet = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = helmetEffect,
                Active = true
            };
            cuirass = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = cuirassEffect,
                Active = true
            };
            greaves = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = greavesEffect,
                Active = true
            };
            gloves = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = glovesEffect,
                Active = true
            };
            boots = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = bootsEffect,
                Active = true
            };
            leftHand = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = leftHandEffect,
                Active = true
            };
            rightHand = new TextureSprite
            {
                ContentFile = "SpriteSheets/Mobs/human_male_white",
                SourceRectangle = new Rectangle2D(Point2D.Empty, Size),
                SpriteSheetEffect = rightHandEffect,
                Active = true
            };

            body.LoadContent();
            helmet.LoadContent();
            cuirass.LoadContent();
            greaves.LoadContent();
            gloves.LoadContent();
            boots.LoadContent();
            leftHand.LoadContent();
            rightHand.LoadContent();

            base.LoadContent();

            body.SpriteSheetEffect.Activate();
            helmet.SpriteSheetEffect.Activate();
            cuirass.SpriteSheetEffect.Activate();
            greaves.SpriteSheetEffect.Activate();
            gloves.SpriteSheetEffect.Activate();
            boots.SpriteSheetEffect.Activate();
            leftHand.SpriteSheetEffect.Activate();
            rightHand.SpriteSheetEffect.Activate();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            body.UnloadContent();
            helmet.UnloadContent();
            cuirass.UnloadContent();
            greaves.UnloadContent();
            gloves.UnloadContent();
            boots.UnloadContent();
            leftHand.UnloadContent();
            rightHand.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            body.Update(gameTime);
            helmet.Update(gameTime);
            cuirass.Update(gameTime);
            greaves.Update(gameTime);
            boots.Update(gameTime);
            gloves.Update(gameTime);
            leftHand.Update(gameTime);
            rightHand.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            body.Draw(spriteBatch);

            if (!mob.Inventory.BootsSlot.IsEmpty)
            {
                boots.Draw(spriteBatch);
            }

            if (!mob.Inventory.LeftHandSlot.IsEmpty)
            {
                leftHand.Draw(spriteBatch);
            }

            if (!mob.Inventory.RightHandSlot.IsEmpty)
            {
                rightHand.Draw(spriteBatch);
            }

            if (!mob.Inventory.GreavesSlot.IsEmpty)
            {
                greaves.Draw(spriteBatch);
            }

            if (!mob.Inventory.CuirassSlot.IsEmpty)
            {
                cuirass.Draw(spriteBatch);
            }

            if (!mob.Inventory.GlovesSlot.IsEmpty)
            {
                gloves.Draw(spriteBatch);
            }

            if (!mob.Inventory.HelmetSlot.IsEmpty)
            {
                helmet.Draw(spriteBatch);
            }
        }

        public void AssociateMob(Mob mob)
        {
            this.mob = mob;
        }

        protected override void SetChildrenProperties()
        {
            base.SetChildrenProperties();

            UpdateEquipmentContentFiles();

            bodyEffect.Action = mob.Action;
            helmetEffect.Action = mob.Action;
            cuirassEffect.Action = mob.Action;
            greavesEffect.Action = mob.Action;
            glovesEffect.Action = mob.Action;
            bootsEffect.Action = mob.Action;
            leftHandEffect.Action = mob.Action;
            rightHandEffect.Action = mob.Action;

            bodyEffect.Direction = mob.Direction;
            helmetEffect.Direction = mob.Direction;
            cuirassEffect.Direction = mob.Direction;
            greavesEffect.Direction = mob.Direction;
            glovesEffect.Direction = mob.Direction;
            bootsEffect.Direction = mob.Direction;
            leftHandEffect.Direction = mob.Direction;
            rightHandEffect.Direction = mob.Direction;

            Point2D location = new Point2D(
                (GameDefines.MAP_TILE_SIZE - body.SourceRectangle.Width) / 2,
                GameDefines.MAP_TILE_SIZE - body.SourceRectangle.Height);

            body.Location = location;
            helmet.Location = location;
            cuirass.Location = location;
            greaves.Location = location;
            gloves.Location = location;
            boots.Location = location;
            leftHand.Location = location;
            rightHand.Location = location;
        }

        void UpdateEquipmentContentFiles()
        {
            if (!mob.Inventory.HelmetSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.HelmetSlot.ItemId}_male";

                if (contentFile != helmet.ContentFile)
                {
                    helmet.ContentFile = contentFile;
                    helmet.LoadContent();
                }
            }

            if (!mob.Inventory.CuirassSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.CuirassSlot.ItemId}_male";

                if (contentFile != cuirass.ContentFile)
                {
                    cuirass.ContentFile = contentFile;
                    cuirass.LoadContent();
                }
            }

            if (!mob.Inventory.GreavesSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.GreavesSlot.ItemId}_male";

                if (contentFile != greaves.ContentFile)
                {
                    greaves.ContentFile = contentFile;
                    greaves.LoadContent();
                }
            }

            if (!mob.Inventory.GlovesSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.GlovesSlot.ItemId}_male";

                if (contentFile != gloves.ContentFile)
                {
                    gloves.ContentFile = contentFile;
                    gloves.LoadContent();
                }
            }

            if (!mob.Inventory.BootsSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.BootsSlot.ItemId}_male";

                if (contentFile != boots.ContentFile)
                {
                    boots.ContentFile = contentFile;
                    boots.LoadContent();
                }
            }

            if (!mob.Inventory.LeftHandSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.LeftHandSlot.ItemId}_male";

                if (contentFile != leftHand.ContentFile)
                {
                    leftHand.ContentFile = contentFile;
                    leftHand.LoadContent();
                }
            }

            if (!mob.Inventory.RightHandSlot.IsEmpty)
            {
                string contentFile = $"SpriteSheets/Equipment/{mob.Inventory.RightHandSlot.ItemId}_male";

                if (contentFile != rightHand.ContentFile)
                {
                    rightHand.ContentFile = contentFile;
                    rightHand.LoadContent();
                }
            }
        }
    }
}