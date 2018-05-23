using NuciXNA.Primitives;

namespace Craftico.Models
{
    public sealed class WorldTile
    {
        public Point2D ChunkLocation { get; set; }

        public Point2D WorldLocation { get; set; }

        public string Id { get; set; }
    }
}
