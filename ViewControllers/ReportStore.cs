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


            Console.Clear();
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
                    Console.WriteLine("\n************ Products **************\n");

                    var products = inventory.Products;
                    if (products.Count == 0)
                    {
                        throw new ProductNotFoundException("No Products Found\n");
                    }
                    else
                    {
                        products.ForEach(product => Console.WriteLine(product));
                    }
                    Console.WriteLine("\n************ Suppliers **************\n");

                    var suppliers = inventory.Suppliers;
                    if (suppliers.Count == 0)
                    {
                        Console.WriteLine("No Suppliers Found\n");
                    }
                    else
                    {
                        suppliers.ForEach(supplier => Console.WriteLine(supplier));
                    }
                    Console.WriteLine("\n************ Transactions **************\n");
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
                var choice = Console.ReadLine();
            }

        }

    }
}
