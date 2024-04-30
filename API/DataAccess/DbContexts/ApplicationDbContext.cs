using DataAccess.Repositories;
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
    public DbSet<ConstructionCompany> ConstructionCompany { get; set; }
    
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    
}