using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class SupplierStore
    {
        private static readonly SupplierRepository _supplierRepository = new SupplierRepository(new InventoryContext());

        public static void DisplaySubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("     SUPPLIER MANAGEMENT MENU       ");
                Console.WriteLine("====================================");
                Console.WriteLine();
                Console.WriteLine("  [1] Add Supplier");
                Console.WriteLine("  [2] Update Supplier");
                Console.WriteLine("  [3] Delete Supplier");
                Console.WriteLine("  [4] View Supplier's Details");
                Console.WriteLine("  [5] View All Suppliers");
                Console.WriteLine("  [6] Go Back To Main Menu");
                Console.WriteLine();
                Console.Write("Enter your ch    oice (1-6): ");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddSupplier();
                            break;
                        case "2":
                            UpdateSupplier();
                            break;
                        case "3":
                            DeleteSupplier();
                            break;
                        case "4":
                            ViewSupplierDetails();
                            break;
                        case "5":
                            ViewAllSuppliers();
                            break;
                        case "6":
                            return;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 6 only...");
                    }
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                    Console.WriteLine("Press Enter to Continue...");
                    var choice = Console.ReadLine();
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Input format is incorrect. Please enter valid data.");
                    Console.WriteLine("Press Enter to Continue...");
                    var choice = Console.ReadLine();
                }
            }
        }

        public static void AddSupplier()
        {
            try
            {
                int inventoryId = GetInventoryId();

                Console.Write("Enter Supplier Name: ");
                string name = Console.ReadLine();


                if (_supplierRepository.SearchSupplierNameInInventory(name, inventoryId) != null)
                {
                    throw new SupplierAlreadyExistsException("Supplier with same name already exists.");
                }

                Console.Write("Enter Supplier Contact Information: ");
                string contactInformation = Console.ReadLine();

                var supplier = new Supplier
                {
                    Name = name,
                    ContactInformation = contactInformation,
                    InventoryId = inventoryId
                };

                _supplierRepository.Add(supplier);
                Console.WriteLine("Supplier added successfully.");
                Console.ReadLine();
            }
            catch (SupplierAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public static void UpdateSupplier()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Suppplier Id to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var supplier = _supplierRepository.GetSupplierById(id, inventoryId);
                if (supplier == null)
                {
                    throw new SupplierNotFoundException("Supplier Not Found...");
                }

                Console.WriteLine("Enter New Supplier Name: ");
                string name = Console.ReadLine();


                if (_supplierRepository.SearchSupplierNameInInventory(name, inventoryId) != null)
                {
                    throw new SupplierAlreadyExistsException("Supplier Already Exists...");
                }

                Console.WriteLine("Enter New Product Contact Information: ");
                string contactInformation = Console.ReadLine();


                supplier.Name = name;
                supplier.ContactInformation = contactInformation;

                _supplierRepository.Update(supplier);
                Console.WriteLine("Supplier Updated Successfully...");
                Console.ReadLine();
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public static void DeleteSupplier()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.Write("Enter Supplier ID to delete: ");

                int id = Convert.ToInt32(Console.ReadLine());

                var supplier = _supplierRepository.GetSupplierById(id, inventoryId);
                if (supplier == null)
                {
                    throw new SupplierNotFoundException("Supplier Not Found...");
                }
                _supplierRepository.Delete(supplier);
                Console.WriteLine("Supplier Deleted Successfully.");
                Console.ReadLine();
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public static void ViewSupplierDetails()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.Write("Enter Supplier ID to View Details: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var supplier = _supplierRepository.GetSupplierById(id, inventoryId);
                if (supplier == null)
                {
                    throw new SupplierNotFoundException("Supplier Not Found...");
                }
                Console.WriteLine(supplier);
                Console.ReadLine();
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
                Console.ReadLine();
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }

        }


        public static void ViewAllSuppliers()
        {
            try
            {
                int inventoryId = GetInventoryId();
                var suppliers = _supplierRepository.GetAll(inventoryId);
                if (suppliers.Count == 0)
                {
                    throw new SupplierNotFoundException("Supplier Not Found...");
                }
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine(supplier);
                }
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

        static int GetInventoryId()
        {
            Console.WriteLine("Enter Inventory Id: ");
            int inventoryId = Convert.ToInt32(Console.ReadLine());

            if (_supplierRepository.SearchInventoryId(inventoryId))
            {
                throw new InventoryNotFoundException("Inventory Not Found");
            }
            return inventoryId;
        }
    }
}
