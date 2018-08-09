namespace Craftico.Models
{
    public class InventorySlot
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(ItemId) || Quantity == 0;

        public string ItemId { get; set; }

        public int Quantity { get; set; }

        public void Empty()
        {
            ItemId = null;
            Quantity = 0;
        }
    }
}
