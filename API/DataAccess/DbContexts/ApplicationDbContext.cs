using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
   
    public DbSet<Category> Categories { get; set; }
    
}