using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts;

public class ApplicationDbContext : DbContext
{
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
    public DbSet<Manager> Managers { get; set; }
    public DbSet<RequestHandler> RequestHandlers { get; set; }
    public DbSet<ConstructionCompanyAdmin> ConstructionCompanyAdmins { get; set; }
    public DbSet<Session> Sessions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MaintenanceRequest>()
            .HasOne(mr => mr.Manager)
            .WithMany(m => m.Requests)
            .HasForeignKey(mr => mr.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}