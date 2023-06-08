using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BongOliver.Models
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Walet> Walets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(p => p.users)
                .WithOne(c => c.role)
                .HasForeignKey(c => c.roleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Walet)
                .WithOne(w => w.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(u => u.User)
                .WithMany(b => b.Bookings);

            modelBuilder.Entity<Service>()
                .HasOne(p => p.ServiceType)
                .WithMany(c => c.Services)
                .HasForeignKey(c => c.serviceTypeId);

            //modelBuilder.Entity<Order>()
            //    .HasMany(e => e.Products)
            //    .WithMany(e => e.Orders);

            //modelBuilder.Entity<Order>()
            //    .HasMany(s => s.Products)
            //    .WithMany(c => c.Orders)
            //    .Map(cs =>
            //        {
            //            cs.MapLeftKey("OrderId");
            //            cs.MapRightKey("ProductId");
            //            cs.ToTable("OrderItem");
            //        });

        }
    }
}
