using InventoryManagement.Exceptions;

namespace InventoryManagement.ViewControllers
{
    internal class MainMenu
    {
        private readonly ProductStore _productStore = new ProductStore();
        private readonly SupplierStore _supplierStore = new SupplierStore();
        private readonly TransactionStore _transactionStore = new TransactionStore();
        private readonly ReportStore _reportStore = new ReportStore();

        public void DisplayMenu()
        {

            while (true)
            {
                Console.Clear(); // Clears the console screen for a clean display
                Console.WriteLine("==================================");
                Console.WriteLine("         INVENTORY SYSTEM         ");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("         === Main Menu ===        ");
                Console.WriteLine();
                Console.WriteLine("  [1] Product Management");
                Console.WriteLine("  [2] Supplier Management");
                Console.WriteLine("  [3] Transaction Management");
                Console.WriteLine("  [4] Generate Report");
                Console.WriteLine("  [5] Exit");
                Console.WriteLine();
                Console.WriteLine("==================================");
                Console.Write("Enter your choice (1-5): ");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            _productStore.DisplaySubMenu();
                            break;
                        case "2":
                            _supplierStore.DisplaySubMenu();
                            break;
                        case "3":
                            _transactionStore.DisplaySubMenu();
                            break;
                        case "4":
                            _reportStore.DisplaySubMenu();
                            break;
                        case "5":
                            Console.WriteLine("Exiting the program. Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 5 only...");
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Input format is incorrect. Please enter valid data." + fe.Message);
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                }
            }
        }
    }
}
