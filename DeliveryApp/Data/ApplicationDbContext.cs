using System;
using System.Collections.Generic;
using System.Text;
using DeliveryApp.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Admin>().ToTable("Admin");
            builder.Entity<Bill>().ToTable("Bill");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Client>().ToTable("Client");
            builder.Entity<DeliveryMan>().ToTable("DeliveryMan");
            builder.Entity<Location>().ToTable("Location");
            builder.Entity<Message>().ToTable("Message");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<ProductImage>().ToTable("ProductImage");
            builder.Entity<ProductOrder>().ToTable("ProductOrder");
            builder.Entity<Rating>().ToTable("Rating");
            builder.Entity<Favorites>().ToTable("Favorites");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        
    }
}
