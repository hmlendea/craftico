namespace Craftico.Models
{
    public class Item
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ItemType Type { get; set; }

        public virtual bool IsEquipable { get; set; }

        public string SpriteSheet { get; set; }

        public Item()
        {
            Type = ItemType.Other;
            IsEquipable = false;
        }
    }
}
