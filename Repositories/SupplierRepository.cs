using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    internal class SupplierRepository
    {
        private readonly InventoryContext _context;

        public SupplierRepository(InventoryContext context)
        {
            _context = context;
        }
        public List<Supplier> GetAll(int id)
        {
            return _context.Suppliers.Where(x => x.InventoryId == id).ToList();
        }
        public void Add(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }

        public void Delete(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
        public Product SearchSupplierNameInInventory(string supplierName, int inventoryId)
        {
            var searchProduct = _context.Products.Where(x => x.Name == supplierName && x.InventoryId == inventoryId).FirstOrDefault();
            return searchProduct;
        }
        public bool SearchInventoryId(int id)
        {
            var searchInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return searchInventory == null;
        }

        public Supplier GetSupplierById(int id, int inventoryId)
        {
            return _context.Suppliers.Where(x => x.SupplierId == id && x.InventoryId == inventoryId).FirstOrDefault();
        }
    }
}
