namespace Craftico.Models
{
    public class Item
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ItemType Type { get; set; }

        public bool IsEquipable
        {
            get
            {
                return Type == ItemType.Helmet ||
                       Type == ItemType.Cuirass ||
                       Type == ItemType.Greaves ||
                       Type == ItemType.Boots ||
                       Type == ItemType.Gloves ||
                       Type == ItemType.Weapon ||
                       Type == ItemType.Shield ||
                       Type == ItemType.Ammo;
            }
        }

        public string SpriteSheet { get; set; }

        public Item()
        {
            Type = ItemType.Other;
        }
    }
}
