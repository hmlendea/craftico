namespace Craftico.Models
{
    public sealed class MobDefinition
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MovementSpeed { get; set; }

        public int InventorySize { get; set; }

        public int VigourLevel { get; set; }

        public int FaithLevel { get; set; }

        public MobDefinition()
        {
            MovementSpeed = 8;
            InventorySize = 28;
            VigourLevel = 10;
            FaithLevel = 1;
        }
    }
}
