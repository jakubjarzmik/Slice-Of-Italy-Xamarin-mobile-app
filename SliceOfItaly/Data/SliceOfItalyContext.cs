using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Data
{
    public class SliceOfItalyContext : DbContext
    {
        public SliceOfItalyContext (DbContextOptions<SliceOfItalyContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; } = default!;

        public DbSet<Address>? Address { get; set; }

        public DbSet<Customer>? Customer { get; set; }

        public DbSet<Dish>? Dish { get; set; }

        public DbSet<Order>? Order { get; set; }

        public DbSet<OrderDish>? OrderDish { get; set; }
    }
}
