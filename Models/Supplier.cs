using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    internal class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }

        public Inventory Inventory { get; set; }


        [ForeignKey("Inventory")]
        public int? InventoryId { get; set; }


        public override string ToString()
        {
            return $"Supplier Id: {SupplierId}\n" +
                $"Inventory Id : {InventoryId}" +
                $"Name: {Name}\n" +
                $"Contact Information: {ContactInformation}\n";
        }
    }
}
