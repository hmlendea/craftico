using NuciXNA.DataAccess.DataObjects;

namespace Craftico.DataAccess.DataObjects
{
    public sealed class TerrainEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SpriteSheet { get; set; }

        public int Order { get; set; }

        public string Colour { get; set; }
    }
}
