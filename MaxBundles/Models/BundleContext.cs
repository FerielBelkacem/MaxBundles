using Microsoft.EntityFrameworkCore;
using MaxBundles.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MaxBundles
{
    public class BundleContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BundlePart> BundleParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string_here");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Bundle>()
                .HasKey(b => b.BundleId);

            modelBuilder.Entity<Bundle>()
                .HasOne(b => b.Product)
                .WithOne()
                .HasForeignKey<Bundle>(b => b.BundleId);

            modelBuilder.Entity<BundlePart>()
                .HasKey(bp => bp.BundlePartId);

            modelBuilder.Entity<BundlePart>()
                .HasOne(bp => bp.Bundle)
                .WithMany(b => b.BundleParts)
                .HasForeignKey(bp => bp.BundleId);

            modelBuilder.Entity<BundlePart>()
                .HasOne(bp => bp.Part)
                .WithMany(p => p.BundleParts)
                .HasForeignKey(bp => bp.PartId);
        }
    }
}
