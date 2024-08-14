using InventoryManagement.ViewControllers;

namespace InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.DisplayMenu();
        }
    }
}
