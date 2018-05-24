using NuciXNA.Primitives;

namespace Craftico.Models
{
    public sealed class Mob
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Point2D Location { get; set; }

        public MobAction Action { get; set; }

        public MobDirection Direction { get; set; }

        public Inventory Inventory { get; set; }

        public Mob()
        {
            Inventory = new Inventory(28);
        }
    }
}
