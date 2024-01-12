using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts;

public class BaseDbContext:DbContext
{
    public DbSet<Product>Products  { get; set; }
    public DbSet<Category> Categories  { get; set; }

    public BaseDbContext(DbContextOptions options):base(options)
    {
        
    }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

  
}
