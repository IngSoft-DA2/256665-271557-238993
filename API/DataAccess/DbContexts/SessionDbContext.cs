using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts;

public class SessionDbContext : DbContext
{
    public SessionDbContext(DbContextOptions<SessionDbContext> options) : base(options)
    {
    }
    
    public DbSet<Session> Sessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}