using Microsoft.EntityFrameworkCore;
using capapi.Models;

namespace capapi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Order> orders { get; set; }
        public DbSet<Shelf> shelves { get; set; }
        public DbSet<OrderLine> orderLines { get; set; }
    }
}
