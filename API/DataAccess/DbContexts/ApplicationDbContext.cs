using System.Net.Sockets;
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
    public DbSet<Owner> Owners { get; set; }
    public DbSet<ConstructionCompany> ConstructionCompany { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Building>()
            .HasOne(b => b.ConstructionCompany)
            .WithMany() 
            .HasForeignKey(b => b.ConstructionCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    
}