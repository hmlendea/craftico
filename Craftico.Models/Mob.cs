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

        public float MovementSpeed { get; set; }

        public Inventory Inventory { get; set; }

        public Skill Vigour { get; set; }

        public Skill Faith { get; set; }

        public Mob()
        {
            MovementSpeed = 0.05f;
            Inventory = new Inventory(28);

            Vigour = new Skill();
            Faith = new Skill();
        }
    }
}
