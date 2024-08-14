using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class ReportStore
    {
        private static readonly InventoryRepository _inventoryRepository = new InventoryRepository(new InventoryContext());

        public void DisplaySubMenu()
        {


            //Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("         GENERATE REPORT MENU       ");
            Console.WriteLine("====================================");
            var inventories = _inventoryRepository.GetAll();
            if (inventories.Count == 0)
            {
                throw new InventoryNotFoundException("No Inventories Found\n");
            }
            else
            {
                inventories.ForEach(inventory =>
                {
                    Console.WriteLine(inventory);
                    Console.WriteLine("List of Products");

                    var products = inventory.Products;
                    if (products.Count == 0)
                    {
                        Console.WriteLine("No Products Found\n");
                    }
                    else
                    {
                        products.ForEach(product => Console.WriteLine(product));
                    }
                    Console.WriteLine("List of Suppliers");
                    var suppliers = inventory.Suppliers;
                    if (suppliers.Count == 0)
                    {
                        Console.WriteLine("No Suppliers Found\n");
                    }
                    else
                    {
                        suppliers.ForEach(supplier => Console.WriteLine(supplier));
                    }
                    Console.WriteLine("List of Transactions");
                    var transactions = inventory.Transactions;
                    if (transactions.Count == 0)
                    {
                        Console.WriteLine("No Transactions Found\n");
                    }
                    else
                    {
                        transactions.ForEach(transaction => Console.WriteLine(transaction));
                    }

                }
                );
            }

        }

    }
}
