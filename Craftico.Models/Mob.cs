using NuciXNA.Primitives;

namespace Craftico.Models
{
    public sealed class Mob
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PointF2D Location { get; set; }

        public MobAction Action { get; set; }

        public MobDirection Direction { get; set; }

        public int MovementSpeed { get; set; }

        public Inventory Inventory { get; set; }

        public Skill Vigour { get; set; }

        public Skill Faith { get; set; }

        public Mob()
        {
            MovementSpeed = 8;
            Inventory = new Inventory(28);

            Vigour = new Skill();
            Faith = new Skill();
        }
    }
}
