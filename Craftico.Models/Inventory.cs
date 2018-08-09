using System.Linq;

namespace Craftico.Models
{
    public sealed class Inventory
    {
        public int Size { get; }

        public InventorySlot HelmetSlot { get; set; }

        public InventorySlot CuirassSlot { get; set; }

        public InventorySlot GreavesSlot { get; set; }

        public InventorySlot GlovesSlot { get; set; }

        public InventorySlot BootsSlot { get; set; }

        public InventorySlot LeftHandSlot { get; set; }

        public InventorySlot RightHandSlot { get; set; }

        public InventorySlot AmmoSlot { get; set; }

        public InventorySlot[] InventorySlots { get; set; }

        public int FreeSlotsCount
        {
            get
            {
                return InventorySlots.Count(x => x == null || x.IsEmpty);
            }
        }

        public Inventory(int size)
        {
            InventorySlots = new InventorySlot[size];
            Size = size;

            HelmetSlot = new InventorySlot();
            CuirassSlot = new InventorySlot();
            GreavesSlot = new InventorySlot();
            GlovesSlot = new InventorySlot();
            BootsSlot = new InventorySlot();
            LeftHandSlot = new InventorySlot();
            RightHandSlot = new InventorySlot();
            AmmoSlot = new InventorySlot();
        }

        public InventorySlot this[int slotIndex]
        {
            get { return InventorySlots[slotIndex]; }
            set { InventorySlots[slotIndex] = value; }
        }
    }
}
