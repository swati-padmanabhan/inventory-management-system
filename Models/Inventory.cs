using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    internal class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        public string Location { get; set; }

        public List<Product> Products { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Transaction> Transactions { get; set; }

        public override string ToString()
        {
            return
                $"\nInventory Id : {InventoryId}\n" +
                $"Inventory Location: {Location}\n";
        }
    }
}
