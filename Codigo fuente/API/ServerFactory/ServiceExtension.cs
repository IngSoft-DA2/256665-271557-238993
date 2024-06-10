using Adapter;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using IAdapter;
using IDataAccess;
using IRepository;
using IServiceLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLogic;


namespace ServerFactory;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryAdapter, CategoryAdapter>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOwnerAdapter, OwnerAdapter>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IConstructionCompanyAdapter, ConstructionCompanyAdapter>();
        services.AddScoped<IConstructionCompanyService, ConstructionCompanyService>();
        services.AddScoped<IConstructionCompanyRepository, ConstructionCompanyRepository>();
        services.AddScoped<IInvitationAdapter, InvitationAdapter>();
        services.AddScoped<IInvitationService, InvitationService>();
        services.AddScoped<IInvitationRepository, InvitationRepository>();
        services.AddScoped<IMaintenanceRequestAdapter, MaintenanceRequestAdapter>();
        services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();
        services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
        services.AddScoped<IBuildingAdapter, BuildingAdapter>();
        services.AddScoped<IBuildingService, BuildingService>();
        services.AddScoped<IFlatAdapter, FlatAdapter>();
        services.AddScoped<IFlatService, FlatService>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IAdministratorAdapter, AdministratorAdapter>();
        services.AddScoped<IAdministratorService, AdministratorService>();
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<IManagerAdapter, ManagerAdapter>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IReportAdapter, ReportAdapter>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IRequestHandlerAdapter, RequestHandlerAdapter>();
        services.AddScoped<IRequestHandlerService, RequestHandlerService>();
        services.AddScoped<IRequestHandlerRepository, RequestHandlerRepository>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IConstructionCompanyAdminAdapter, ConstructionCompanyAdminAdapter>();
        services.AddScoped<IConstructionCompanyAdminService, ConstructionCompanyAdminService>();
        services.AddScoped<IConstructionCompanyAdminRepository, ConstructionCompanyAdminRepository>();
    }

    public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DbContext, ApplicationDbContext>(dbContext =>
            dbContext.UseSqlServer(connectionString));
    }
}