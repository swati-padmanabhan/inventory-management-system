using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class ProductStore
    {
        private static readonly ProductRepository _productRepository = new ProductRepository(new InventoryContext());


        public void DisplaySubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("      PRODUCT MANAGEMENT MENU     ");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("  [1] Add Product");
                Console.WriteLine("  [2] Update Product");
                Console.WriteLine("  [3] Delete Product");
                Console.WriteLine("  [4] View Product's Details");
                Console.WriteLine("  [5] View All Products");
                Console.WriteLine("  [6] Go Back To Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice (1-6): ");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            UpdateProduct();
                            break;
                        case "3":
                            DeleteProduct();
                            break;
                        case "4":
                            ViewProductDetails();
                            break;
                        case "5":
                            ViewAllProducts();
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

        public void AddProduct()
        {
            try
            {
                int inventoryId = GetInventoryId();


                Console.WriteLine("Enter Product Name: ");
                string name = Console.ReadLine();

                if (_productRepository.SearchProductNameInInventory(name, inventoryId) != null)
                {
                    throw new ProductAlreadyExistsException("Product with same name already exists.");
                }

                Console.WriteLine("Enter Product Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter Product Quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                if (quantity <= 0)
                {
                    throw new InvalidValueException("Quantity must be greater than zero.");
                }

                Console.WriteLine("Enter Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                if (price <= 0)
                {
                    throw new InvalidValueException("Price must be a positive number.");
                }

                var product = new Product
                {
                    Name = name,
                    Description = description,
                    Quantity = quantity,
                    Price = price,
                    InventoryId = inventoryId
                };
                _productRepository.Add(product);

                Console.WriteLine("Product added successfully.");
                Console.ReadLine();
            }
            catch (ProductAlreadyExistsException ex)
            {
                Console.WriteLine("Product with same name already exists.");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
            catch (InvalidValueException ive)
            {
                Console.WriteLine(ive.Message);
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

        public void UpdateProduct()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Product Id to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());


                var product = _productRepository.GetProductById(id, inventoryId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }

                Console.WriteLine("Enter New Product Name: ");
                string name = Console.ReadLine();

                if (_productRepository.SearchProductNameInInventory(name, inventoryId) != null)
                {
                    throw new ProductAlreadyExistsException("Product Already Exists...");
                }

                Console.WriteLine("Enter New Product Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter New Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                if (price <= 0)
                {
                    throw new InvalidValueException("Price must be a positive number.");
                }

                product.Name = name;
                product.Description = description;
                product.Price = price;

                _productRepository.Update(product);
                Console.WriteLine("Product Updated Successfully...");
                Console.ReadLine();

            }
            catch (ProductNotFoundException pfe)
            {
                Console.WriteLine(pfe.Message);
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

        public void DeleteProduct()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.WriteLine("Enter Product ID to delete:");
                int id = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetProductById(id, inventoryId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }

                _productRepository.Delete(product);

                Console.WriteLine("Product deleted successfully.");
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

        public void ViewProductDetails()
        {
            try
            {
                int inventoryId = GetInventoryId();
                Console.Write("Enter Product ID to View Details: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetProductById(id, inventoryId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }
                Console.WriteLine(product);
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


        public void ViewAllProducts()
        {
            try
            {
                int inventoryId = GetInventoryId();
                var products = _productRepository.GetAll(inventoryId);
                if (products.Count == 0)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }
                foreach (var product in products)
                {
                    Console.WriteLine(product);
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

            if (_productRepository.SearchInventoryId(inventoryId))
            {
                throw new InventoryNotFoundException("Inventory Not Found");
            }
            return inventoryId;
        }


    }

}

