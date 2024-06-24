using System;
using System.Collections.Generic;
using System.Linq;
using MaxBundles.Models; 
using MaxBundles;

namespace BundleBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new BundleContext())
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
                    new Product { ProductName = "Seat", ProductType = ProductType.Part, InventoryCount = 50 },
                    new Product { ProductName = "Pedal", ProductType = ProductType.Part, InventoryCount = 60 },
                    new Product { ProductName = "Frame", ProductType = ProductType.Part, InventoryCount = 60 },
                    new Product { ProductName = "Tube", ProductType = ProductType.Part, InventoryCount = 35 },
                    new Product { ProductName = "Wheel", ProductType = ProductType.Bundle },
                    new Product { ProductName = "Bike", ProductType = ProductType.Bundle }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

                // Create wheel bundle
                var wheel = context.Products.First(p => p.ProductName == "Wheel");
                var frame = context.Products.First(p => p.ProductName == "Frame");
                var tube = context.Products.First(p => p.ProductName == "Tube");

                var wheelBundle = new Bundle
                {
                    Product = wheel,
                    BundleParts = new List<BundlePart>
                    {
                        new BundlePart { Part = frame, Quantity = 1 },
                        new BundlePart { Part = tube, Quantity = 1 }
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
                    Product = bike,
                    BundleParts = new List<BundlePart>
                    {
                        new BundlePart { Part = seat, Quantity = 1 },
                        new BundlePart { Part = pedal, Quantity = 2 },
                        new BundlePart { Part = wheel, Quantity = 2 }
                    }
                };

                context.Bundles.Add(bikeBundle);
                context.SaveChanges();

                // Calculate the maximum number of bikes that can be built
                int maxBikes = CalculateMaxBundles(bikeBundle.BundleParts, inventory);

                Console.WriteLine($"Maximum number of bikes that can be built: {maxBikes}");
                Console.ReadLine();
            }
        }

        public static int CalculateMaxBundles(ICollection<BundlePart> bundleParts, Dictionary<string, int> inventory)
        {
            int maxBundles = int.MaxValue;

            foreach (var bundlePart in bundleParts)
            {
                string partName = bundlePart.Part.ProductName;
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
