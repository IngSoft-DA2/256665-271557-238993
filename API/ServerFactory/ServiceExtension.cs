using DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ServerFactory;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
    }

    public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DbContext, ApplicationDbContext>(dbContext =>
            dbContext.UseSqlServer(connectionString));
    }
}