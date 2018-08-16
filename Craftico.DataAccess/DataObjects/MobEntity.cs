using NuciXNA.DataAccess.DataObjects;

namespace Craftico.DataAccess.DataObjects
{
    public sealed class MobEntity : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int MovementSpeed { get; set; }

        public int InventorySize { get; set; }

        public int VigourLevel { get; set; }

        public int FaithLevel { get; set; }
    }
}
