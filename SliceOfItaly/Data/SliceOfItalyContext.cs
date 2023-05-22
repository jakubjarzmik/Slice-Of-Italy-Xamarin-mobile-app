using Microsoft.EntityFrameworkCore;

namespace SliceOfItaly.Data
{
    public class SliceOfItalyContext : DbContext
    {
        public SliceOfItalyContext (DbContextOptions<SliceOfItalyContext> options)
            : base(options)
        {
        }

        public DbSet<SliceOfItaly.Models.Category> Category { get; set; } = default!;

        public DbSet<SliceOfItaly.Models.Address>? Address { get; set; }

        public DbSet<SliceOfItaly.Models.Customer>? Customer { get; set; }

        public DbSet<SliceOfItaly.Models.Dish>? Dish { get; set; }

        public DbSet<SliceOfItaly.Models.Order>? Order { get; set; }

        public DbSet<SliceOfItaly.Models.OrderDish>? OrderDish { get; set; }
    }
}
