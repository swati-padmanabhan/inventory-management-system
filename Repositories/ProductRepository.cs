using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    internal class ProductRepository
    {
        private readonly InventoryContext _context;

        public ProductRepository(InventoryContext context)
        {
            _context = context;
        }

        public List<Product> GetAll(int id)
        {
            return _context.Products.Where(x => x.InventoryId == id).ToList();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public Product SearchProductNameInInventory(string productName, int inventoryId)
        {
            var searchProduct = _context.Products.Where(x => x.Name == productName && x.InventoryId == inventoryId).FirstOrDefault();
            return searchProduct;
        }
        public bool SearchInventoryId(int id)
        {
            var searchInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return searchInventory == null;
        }
        public Product GetProductById(int id, int inventoryId)
        {
            return _context.Products.Where(x => x.ProductId == id && x.InventoryId == inventoryId).FirstOrDefault();

        }
    }
}
