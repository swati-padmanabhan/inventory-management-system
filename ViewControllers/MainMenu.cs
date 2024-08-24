using InventoryManagement.Exceptions;

namespace InventoryManagement.ViewControllers
{
    internal class MainMenu
    {

        public static void DisplayMenu()
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
                            ProductStore.DisplaySubMenu();
                            break;
                        case "2":
                            SupplierStore.DisplaySubMenu();
                            break;
                        case "3":
                            TransactionStore.DisplaySubMenu();
                            break;
                        case "4":
                            ReportStore.DisplaySubMenu();
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
                    Console.WriteLine(fe.Message);
                    Console.WriteLine("Press Enter to Continue...");
                    var choice = Console.ReadLine();
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                    Console.WriteLine("Press Enter to Continue...");
                    var choice = Console.ReadLine();

                }
            }
        }
    }
}
