using CarShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Data.Context
{
    public class CarShopContext : DbContext
    {
        public CarShopContext(DbContextOptions<CarShopContext> options) : base(options)
        {

        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(c =>
            {
                c.HasIndex(r => r.LicensePlate).IsUnique();
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
