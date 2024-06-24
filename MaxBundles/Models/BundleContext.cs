using Microsoft.EntityFrameworkCore;
using MaxBundles.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;

namespace MaxBundles
{
    public class BundleContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BundleContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BundlePart> BundleParts { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bundle>()
                .HasMany(b => b.BundleParts)
                .WithOne(bp => bp.Bundle)
                .HasForeignKey(bp => bp.BundleId);

            modelBuilder.Entity<BundlePart>()
                .HasOne(bp => bp.Product)
                .WithMany(p => p.BundleParts)
                .HasForeignKey(bp => bp.ProductId);

            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.ProductId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}