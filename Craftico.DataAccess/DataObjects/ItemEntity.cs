using NuciXNA.DataAccess.DataObjects;

namespace Craftico.DataAccess.DataObjects
{
    public sealed class ItemEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string SpriteSheet { get; set; }
    }
}
