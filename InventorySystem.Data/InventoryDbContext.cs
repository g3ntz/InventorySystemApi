using Microsoft.EntityFrameworkCore;
using InventorySystem.Core.Models;
using System.Linq;
using InventorySystem.Data.Configurations;

namespace InventorySystem.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { this.ChangeTracker.LazyLoadingEnabled = false; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new ProductDetailsConfigurations());
        }

        public DbSet<History> History { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryStatus> InventoryStatus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
    }
}
