using Microsoft.EntityFrameworkCore;

namespace efcore_tenancy.Data
{
    public class ProductsDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}