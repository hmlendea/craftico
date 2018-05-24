namespace Craftico.Models
{
    public class Wearable : Item
    {
        public EquipmentSlot EquipmentSlot { get; set; }

        public int ArmourRating { get; set; }

        public Wearable()
        {
            IsEquipable = true;
            Type = ItemType.Wearable;
        }
    }
}
