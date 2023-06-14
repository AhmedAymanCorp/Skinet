using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) 
    : base(options)
    {
    }

    // DbSets for Tables 
    public DbSet<Product> Products { get; set; }
}
