using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using NuciXNA.Gui.GuiElements;
using NuciXNA.Primitives;

using Craftico.GameLogic.GameManagers;
using Craftico.Models;
using Craftico.Settings;

namespace Craftico.Gui.GuiElements
{
    public class GuiWorldObject : GuiElement
    {
        IEntityManager entities;

        List<GuiImage> layerImages;
        List<Point2D> offsets;

        public string WorldObjectId { get; }

        public GuiWorldObject(IEntityManager entities, string worldObjectId)
        {
            this.entities = entities;

            WorldObjectId = worldObjectId;
        }

        public override void LoadContent()
        {
            layerImages = new List<GuiImage>();
            offsets = new List<Point2D>();

            WorldObject worldObject = entities.GetWorldObject(WorldObjectId);

            foreach (WorldObjectLayer layer in worldObject.Layers)
            {
                Size2D size = new Size2D(
                        layer.Size.Width * GameDefines.MAP_TILE_SIZE,
                        layer.Size.Height * GameDefines.MAP_TILE_SIZE);

                GuiImage layerImage = new GuiImage
                {
                    ContentFile = $"SpriteSheets/WorldObjects/{layer.SpriteSheet}",
                    SourceRectangle = new Rectangle2D(
                        (layer.SpriteIndex % layer.Size.Width) * size.Width,
                        (layer.SpriteIndex / layer.Size.Width) * size.Height,
                        size.Width,
                        size.Height),
                    Size = size
                };

                layerImage.LoadContent();

                layerImages.Add(layerImage);
                offsets.Add(new Point2D(
                    (layer.Location.X - layer.Anchor.X) * GameDefines.MAP_TILE_SIZE,
                    (layer.Location.Y - layer.Anchor.Y) * GameDefines.MAP_TILE_SIZE));
            }

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            layerImages.ForEach(x => x.UnloadContent());

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            layerImages.ForEach(x => x.Update(gameTime));

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            layerImages.ForEach(x => x.Draw(spriteBatch));

            base.Draw(spriteBatch);
        }

        protected override void SetChildrenProperties()
        {
            for (int i = 0; i < layerImages.Count; i++)
            {
                layerImages[i].Location = new Point2D(
                    Location.X + offsets[i].X,
                    Location.Y + offsets[i].Y);
            }

            base.SetChildrenProperties();
        }
    }
}
