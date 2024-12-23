using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            Console.WriteLine("Select a strategy:");
            Console.WriteLine("1. Lazy Loading");
            Console.WriteLine("2. Eager Loading");
            Console.WriteLine("3. Explicit Loading");
            Console.WriteLine("4. LINQ Query");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    LazyLoadingExample(context);
                    break;
                case "2":
                    EagerLoadingExample(context);
                    break;
                case "3":
                    ExplicitLoadingExample(context);
                    break;
                case "4":
                    LINQQueryExample(context);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void LazyLoadingExample(ApplicationDbContext context)
    {
        var product = context.Products.FirstOrDefault();
        Console.WriteLine($"Product: {product.Name}, Category: {product.Category?.Name}");
    }

    static void EagerLoadingExample(ApplicationDbContext context)
    {
        var products = context.Products.Include(p => p.Category).ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Category: {product.Category?.Name}");
        }
    }

    static void ExplicitLoadingExample(ApplicationDbContext context)
    {
        var product = context.Products.FirstOrDefault();
        context.Entry(product).Reference(p => p.Category).Load();
        Console.WriteLine($"Product: {product.Name}, Category: {product.Category?.Name}");
    }

    static void LINQQueryExample(ApplicationDbContext context)
    {
        var products = context.Products
            .Where(p => p.Price > 100)
            .OrderBy(p => p.Price)
            .ToList();

        foreach (var product in products)
        {
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
        }

        var totalSum = context.Products.Sum(p => p.Price);
        Console.WriteLine($"Total sum of all products: {totalSum}");
    }
}
