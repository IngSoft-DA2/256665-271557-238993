using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Repositories.Context;

public class AuthorizationDbContext : IdentityDbContext
{
    public AuthorizationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRoleId = "a9982ab5-aa3f-4b42-971f-b1dffce772bd";
        var managerRoleId = "d19a267f-d052-4d88-9f11-f881e44e6d35";
        var requestHandlerRoleId = "1d9f5e31-9034-4503-ab39-251fe75a8aba";
        
        // create admin, manager and request-handler roles
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                concurrencyStamp = adminRoleId
            },
            new IdentityRole
            {
                Id = managerRoleId,
                Name = "Manager",
                NormalizedName = "MANAGER",
                concurrencyStamp = managerRoleId
            },
            new IdentityRole
            {
                Id = requestHandlerRoleId,
                Name = "RequestHandler",
                NormalizedName = "REQUESTHANDLER",
                concurrencyStamp = requestHandlerRoleId
            }
        };
    }
}