using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    internal class TransactionRepository
    {
        private readonly InventoryContext _context;

        public TransactionRepository(InventoryContext context)
        {
            _context = context;
        }

        public List<Transaction> ViewLog(int id, int inventoryId)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }
            return _context.Transactions
                           .Where(t => t.ProductId == id && t.InventoryId == inventoryId)
                           .OrderBy(t => t.Date)
                           .ToList();
        }


        public void Add(int id, int quantity, int inventoryId)
        {
            var product = GetProductById(id, inventoryId);
            product.Quantity += quantity;
            var transaction = new Transaction
            {
                ProductId = id,
                Type = "Add Stock",
                Quantity = quantity,
                Date = DateTime.Now,
                InventoryId = inventoryId
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Remove(int id, int quantity, int inventoryId)
        {
            var product = GetProductById(id, inventoryId);
            product.Quantity -= quantity;
            var transaction = new Transaction
            {
                ProductId = id,
                Type = "Remove Stock",
                Quantity = quantity,
                Date = DateTime.Now,
                InventoryId = inventoryId
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }


        public Product GetProductById(int id, int inventoryId)
        {
            return _context.Products.Where(x => x.ProductId == id && x.InventoryId == inventoryId).FirstOrDefault();
        }

        public bool SearchInventoryId(int id)
        {
            var searchInventory = _context.Inventories.Where(x => x.InventoryId == id).FirstOrDefault();
            return searchInventory == null;
        }

    }
}
