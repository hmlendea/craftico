﻿using Microsoft.Xna.Framework;
using NuciXNA.Graphics.SpriteEffects;
using NuciXNA.Primitives;

using Craftico.Models;

namespace Craftico.Gui.MobAnimationEffects
{
    public class TerrainSpriteSheetEffect : SpriteSheetEffect
    {
        public TerrainVariation Variation { get; set; }

        public TerrainSpriteSheetEffect()
        {
            FrameAmount = new Size2D(3, 6);
        }

        public override void UpdateFrame(GameTime gameTime)
        {
            CurrentFrame = new Point2D(
                (int)Variation % FrameAmount.Width,
                (int)Variation / FrameAmount.Width);
        }
    }
}
