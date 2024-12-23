using ComputerPartsShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ComputerPartsShop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Зчитування конфігурацій
            var config = new ConfigurationBuilder()
                .SetBasePath(@"C:\Path\To\Your\Project")
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                while (true)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Display Categories");
                    Console.WriteLine("2. Display Products");
                    Console.WriteLine("3. Add Category");
                    Console.WriteLine("4. Add Product");
                    Console.WriteLine("5. Update Category");
                    Console.WriteLine("6. Update Product");
                    Console.WriteLine("7. Delete Category");
                    Console.WriteLine("8. Delete Product");
                    Console.WriteLine("9. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayCategories(context);
                            break;
                        case "2":
                            DisplayProducts(context);
                            break;
                        case "3":
                            AddCategory(context);
                            break;
                        case "4":
                            AddProduct(context);
                            break;
                        case "5":
                            UpdateCategory(context);
                            break;
                        case "6":
                            UpdateProduct(context);
                            break;
                        case "7":
                            DeleteCategory(context);
                            break;
                        case "8":
                            DeleteProduct(context);
                            break;
                        case "9":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        static void DisplayCategories(ApplicationDbContext context)
        {
            var categories = context.Categories.ToList();
            Console.WriteLine("\nCategories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryId}, Name: {category.Name}");
            }
        }

        static void DisplayProducts(ApplicationDbContext context)
        {
            var products = context.Products.Include(p => p.Category).ToList();
            Console.WriteLine("\nProducts:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}, Category: {product.Category?.Name}");
            }
        }

        static void AddCategory(ApplicationDbContext context)
        {
            Console.WriteLine("Enter category name:");
            string name = Console.ReadLine();

            var category = new Category { Name = name };
            context.Categories.Add(category);
            context.SaveChanges();

            Console.WriteLine("Category added successfully.");
        }

        static void AddProduct(ApplicationDbContext context)
        {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter product price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter category ID:");
            int categoryId = int.Parse(Console.ReadLine());

            var product = new Product { Name = name, Price = price, CategoryId = categoryId };
            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine("Product added successfully.");
        }

        static void UpdateCategory(ApplicationDbContext context)
        {
            Console.WriteLine("Enter category ID to update:");
            int id = int.Parse(Console.ReadLine());

            var category = context.Categories.Find(id);
            if (category != null)
            {
                Console.WriteLine("Enter new name (leave empty to keep current):");
                string newName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newName))
                    category.Name = newName;

                context.SaveChanges();
                Console.WriteLine("Category updated successfully.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        static void UpdateProduct(ApplicationDbContext context)
        {
            Console.WriteLine("Enter product ID to update:");
            int id = int.Parse(Console.ReadLine());

            var product = context.Products.Find(id);
            if (product != null)
            {
                Console.WriteLine("Enter new name (leave empty to keep current):");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    product.Name = newName;

                Console.WriteLine("Enter new price (leave empty to keep current):");
                string newPriceInput = Console.ReadLine();
                if (decimal.TryParse(newPriceInput, out decimal newPrice))
                    product.Price = newPrice;

                context.SaveChanges();
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void DeleteCategory(ApplicationDbContext context)
        {
            Console.WriteLine("Enter category ID to delete:");
            int id = int.Parse(Console.ReadLine());

            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                Console.WriteLine("Category deleted successfully.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        static void DeleteProduct(ApplicationDbContext context)
        {
            Console.WriteLine("Enter product ID to delete:");
            int id = int.Parse(Console.ReadLine());

            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }
}
