using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<CategoryComponent> Categories { get; set; }
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

        modelBuilder.Entity<CategoryComponent>()
            .HasDiscriminator<string>("CategoryType")
            .HasValue<Category>("Leaf")
            .HasValue<CategoryComposite>("Composite");

        modelBuilder.Entity<CategoryComposite>()
            .HasMany(c => c.SubCategories)
            .WithOne()
            .HasForeignKey(c => c.CategoryFatherId)
            .OnDelete(DeleteBehavior.Restrict);

        var adminId = new Guid("E1A402B9-6760-46BC-8362-7CFDEDA9F162"); 
        modelBuilder.Entity<Administrator>().HasData(
            new Administrator()
            {
                Id = adminId,
                Firstname = "seedAdminName",
                Lastname = "seedAdminLastName",
                Email = "seedAdmin@example.com",
                Password = "seedAdminPassword",
                Role = SystemUserRoleEnum.Admin,
                Invitations = new List<Invitation>()
            });
        }
    
    
}