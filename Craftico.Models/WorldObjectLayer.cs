using NuciXNA.Primitives;

namespace Craftico.Models
{
    public sealed class WorldObjectLayer
    {
        public string SpriteSheet { get; set; }

        public int SpriteIndex { get; set; }

        public Size2D Size { get; set; }

        public Point2D Anchor { get; set; }

        public Point2D Location { get; set; }
    }
}
