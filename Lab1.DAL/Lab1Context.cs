using Microsoft.EntityFrameworkCore;
using Lab1.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Lab1.DAL {
    public class Lab1Context : IdentityDbContext<IdentityUser> {
        public virtual DbSet<Product> Products { get; set; }
        public Lab1Context(DbContextOptions<Lab1Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //primary key
            modelBuilder.Entity<Product>()
                .HasKey(b => b.ProductId);

            //property constraints
            modelBuilder.Entity<Product>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(b => b.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(b => b.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(b => b.Category)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(b => b.Quantity)
                .IsRequired();
        }
    }
}
