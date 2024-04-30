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
    
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<RequestHandler> RequestHandlers { get; set; }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         string directory = Directory.GetCurrentDirectory();
    //         IConfigurationRoot configuration = new ConfigurationBuilder()
    //             .SetBasePath(directory)
    //             .AddJsonFile("appsettings.json")
    //             .Build();
    //         var connectionString = configuration.GetConnectionString("DefaultConnection");
    //         optionsBuilder.UseSqlServer(connectionString);
    //     }
    // }
}