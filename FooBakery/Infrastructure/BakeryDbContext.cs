namespace FooBakery.Infrastructure {
    using System.Data.Entity;
    using Models;

    class BakeryDbContext : DbContext {
        public DbSet<Order> Orders { get; set; }
    }
}