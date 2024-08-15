using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class TransactionStore
    {
        private readonly InventoryContext _context;
        private static readonly TransactionRepository _transactionRepository = new TransactionRepository(new InventoryContext());


        public void DisplaySubMenu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("====================================");
                    Console.WriteLine("   TRANSACTION MANAGEMENT MENU     ");
                    Console.WriteLine("====================================");
                    Console.WriteLine();
                    Console.WriteLine("  [1] Add Stock");
                    Console.WriteLine("  [2] Remove Stock");
                    Console.WriteLine("  [3] View Transaction History");
                    Console.WriteLine("  [4] Go Back To Main Menu");
                    Console.WriteLine();
                    Console.Write("Enter your choice (1-4): ");


                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddStock();
                            break;
                        case "2":
                            RemoveStock();
                            break;
                        case "3":
                            ViewTransactionHistory();
                            break;
                        case "4":
                            return; // Go back to the main menu
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 4 only...");

                    }
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                    Console.WriteLine("Press Enter to Continue...");
                    Console.ReadLine();
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Input format is incorrect. Please enter valid data.");
                    Console.WriteLine("Press Enter to Continue...");
                    Console.ReadLine();
                }
            }
        }

        public void AddStock()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Product ID to Add Stock:");
                int productId = Convert.ToInt32(Console.ReadLine());

                var product = _transactionRepository.GetProductById(productId, inventoryId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product not found.");
                }

                Console.WriteLine("Enter Quantity to be Added:");
                int quantityToAdd = Convert.ToInt32(Console.ReadLine());

                if (quantityToAdd < 1)
                {
                    throw new InvalidValueException("Quantity must be greater than zero.");
                }

                _transactionRepository.Add(productId, quantityToAdd, inventoryId);

                Console.WriteLine("Stock added successfully.");
                Console.ReadLine();

            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public void RemoveStock()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Product ID to Remove Stock:");
                int productId = Convert.ToInt32(Console.ReadLine());

                var product = _transactionRepository.GetProductById(productId, inventoryId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product not found.");
                }

                Console.WriteLine("Enter Quantity to be Removed:");
                int quantityToRemove = Convert.ToInt32(Console.ReadLine());

                if (quantityToRemove < 1)
                {
                    throw new InvalidValueException("Quantity must be greater than zero.");
                }


                if (product.Quantity < quantityToRemove)
                {
                    Console.WriteLine("Insufficient stock.");
                    return;
                }
                _transactionRepository.Remove(productId, quantityToRemove, inventoryId);
                Console.WriteLine("Stock removed successfully.");
                Console.ReadLine();
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public void ViewTransactionHistory()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Product ID: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                var transactions = _transactionRepository.ViewLog(productId, inventoryId);
                transactions.ForEach(transaction => Console.WriteLine(transaction));

                Console.WriteLine("Press Enter to Continue...");
                var choice = Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }


        public int GetInventoryId()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_transactionRepository.SearchInventoryId(inventoryId))
            {
                throw new InventoryNotFoundException("Inventory Not Found");
            }
            return inventoryId;


        }

    }
}
