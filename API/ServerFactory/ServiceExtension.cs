using Adapter;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using IAdapter;
using IRepository;
using IServiceLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLogic;


namespace ServerFactory;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICategoryAdapter,CategoryAdapter>();
        services.AddScoped<ICategoryService , CategoryService>();
        services.AddScoped<ICategoryRepository , CategoryRepository>();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        services.AddScoped<IInvitationAdapter, InvitationAdapter>();
        services.AddScoped<IInvitationService, InvitationService>();
        services.AddScoped<IInvitationRepository, InvitationRepository>();
        
        
        services.AddDbContext<DbContext, ApplicationDbContext>(o => o.UseSqlServer(connectionString));
    }

    public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DbContext, ApplicationDbContext>(dbContext =>
            dbContext.UseSqlServer(connectionString));
    }
}