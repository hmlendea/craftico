using NuciXNA.Primitives;

namespace Craftico.Models
{
    public class Terrain
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SpriteSheet { get; set; }

        public int Order { get; set; }

        public Colour Colour { get; set; }
    }
}
