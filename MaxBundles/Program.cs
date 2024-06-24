using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MaxBundles.Models;
using MaxBundles;

namespace BundleBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BundleContext>();
                InitializeData(context);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<BundleContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                });

        public static void InitializeData(BundleContext context)
        {
            // Initialize inventory
            Dictionary<string, int> inventory = new Dictionary<string, int>
            {
                {"Seat", 50},
                {"Pedal", 60},
                {"Frame", 60},
                {"Tube", 35}
            };

            // Create products and add to context
            var products = new List<Product>
            {
                new Product { ProductName = "Seat", ProductType = 0 }, // 0 for Part
                new Product { ProductName = "Pedal", ProductType = 0 },
                new Product { ProductName = "Frame", ProductType = 0 },
                new Product { ProductName = "Tube", ProductType = 0 },
                new Product { ProductName = "Wheel", ProductType = 1 }, // 1 for Bundle
                new Product { ProductName = "Bike", ProductType = 1 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // Create wheel bundle
            var wheel = context.Products.First(p => p.ProductName == "Wheel");
            var frame = context.Products.First(p => p.ProductName == "Frame");
            var tube = context.Products.First(p => p.ProductName == "Tube");

            var wheelBundle = new Bundle
            {
                Name = "Wheel Bundle",
                Description = "A bundle for Wheel",
                BundleParts = new List<BundlePart>
                {
                    new BundlePart { Product = frame, Quantity = 1 },
                    new BundlePart { Product = tube, Quantity = 1 }
                }
            };

            context.Bundles.Add(wheelBundle);
            context.SaveChanges();

            // Calculate the maximum number of wheels that can be built
            int maxWheels = CalculateMaxBundles(wheelBundle.BundleParts, inventory);
            inventory["Wheel"] = maxWheels;

            // Create bike bundle
            var bike = context.Products.First(p => p.ProductName == "Bike");
            var seat = context.Products.First(p => p.ProductName == "Seat");
            var pedal = context.Products.First(p => p.ProductName == "Pedal");

            var bikeBundle = new Bundle
            {
                Name = "Bike Bundle",
                Description = "A bundle for Bike",
                BundleParts = new List<BundlePart>
                {
                    new BundlePart { Product = seat, Quantity = 1 },
                    new BundlePart { Product = pedal, Quantity = 2 },
                    new BundlePart { Product = wheel, Quantity = 2 }
                }
            };

            context.Bundles.Add(bikeBundle);
            context.SaveChanges();

            // Calculate the maximum number of bikes that can be built
            int maxBikes = CalculateMaxBundles(bikeBundle.BundleParts, inventory);

            Console.WriteLine($"Maximum number of bikes that can be built: {maxBikes}");
            Console.ReadLine();
        }

        public static int CalculateMaxBundles(ICollection<BundlePart> bundleParts, Dictionary<string, int> inventory)
        {
            int maxBundles = int.MaxValue;

            foreach (var bundlePart in bundleParts)
            {
                string partName = bundlePart.Product.ProductName;
                int requiredQuantity = bundlePart.Quantity;

                if (!inventory.ContainsKey(partName))
                {
                    return 0;
                }

                int availableQuantity = inventory[partName];
                int possibleBundles = availableQuantity / requiredQuantity;

                if (possibleBundles < maxBundles)
                {
                    maxBundles = possibleBundles;
                }
            }

            return maxBundles;
        }
    }
}
