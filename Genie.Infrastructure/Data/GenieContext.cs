using Genie.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Genie.Infrastructure.Data
{
    public class GenieContext : DbContext
    {
        public GenieContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}