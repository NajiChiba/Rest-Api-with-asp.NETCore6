using Heros.Models;
using Microsoft.EntityFrameworkCore;

namespace Heros.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Hero> Heroes { get; set; }
}