using capproj.Models;
using Microsoft.EntityFrameworkCore;

namespace capproj.Data
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions options) : base(options) { }
        // helpful strongly-typed ctor commonly used by DI
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderLines> orderLines { get; set; }
        public DbSet<Shelf> shelves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure OrderLines as a regular entity with its own primary key
            modelBuilder.Entity<OrderLines>(ol =>
            {
                ol.HasKey(x => new {x.OrderId, x.ShelfId});

                ol.HasOne(x => x.order)
                  .WithMany(o => o.orderLines)
                  .HasForeignKey(x => x.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

                ol.HasOne(x => x.shelf)
                  .WithMany(s => s.orderLines)
                  .HasForeignKey(x => x.ShelfId)
                  .OnDelete(DeleteBehavior.Restrict);

                ol.Property(x => x.Quantity).IsRequired();
            });

            // Basic constraints for other entities (optional, but helpful)
            modelBuilder.Entity<Shelf>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
            });
        }
    }
}
