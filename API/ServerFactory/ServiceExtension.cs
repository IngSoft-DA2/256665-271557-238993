using Adapter;
using IAdapter;
using IServiceLogic;
using Microsoft.Extensions.DependencyInjection;
using ServiceLogic;

namespace ServerFactory;

public static class ServiceExtension
{
   public static IServiceCollection AddServices(this IServiceCollection services)
   {
       //SERVICES
      services.AddScoped<IInvitationService , InvitationService>();
      //services.AddScoped<IInvitationRepository , InvitationRepository>();
      
      services.AddScoped<ICategoryService , CategoryService>();
      //services.AddScoped<ICategoryRepository , CategoryRepository>();
      
      services.AddScoped<IConstructionCompanyService , ConstructionCompanyService>();
      //services.AddScoped<IConstructionCompanyRepository , ConstructionCompanyRepository>();
      
      services.AddScoped<IOwnerService , OwnerService>();
      //services.AddScoped<IOwnerRepository, OwnerRepository>();
      
      services.AddScoped<IBuildingService , BuildingService>();
      //services.AddScoped<IBuildingRepository , BuildingRepository>();
      
      services.AddScoped<IFlatService , FlatService>();
      //services.AddScoped<IFlatRepository , FlatRepository>();
      
      services.AddScoped<IMaintenanceRequestService , MaintenanceRequestService>();
      //services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();

      //services.AddScoped<IRequestHandlerService, RequestHandlerService>();
      //services.AddScoped<IRequestHandlerRepository, RequestHandlerRepository>();
      
      services.AddScoped<IManagerService , ManagerService>();
      //services.AddScoped<IManagerRepository, ManagerRepository>();

      services.AddScoped<IReportService , ReportService>();
      //services.AddScoped<IReportRepository, ReportRepository>();

      services.AddScoped<IAdministratorService, AdministratorService>();
      //services.AddScoped<IAdministratorRepository, AdministratorRepository>();
      
      //--------------------------------------------------------------------------------------
      //ADAPTERS
      
     services.AddScoped<IInvitationAdapter , InvitationAdapter>();
     //services.AddScoped<IInvitationRepository , InvitationRepository>();
      
     services.AddScoped<ICategoryAdapter , CategoryAdapter>();
     //services.AddScoped<ICategoryRepository , CategoryRepository>();
      
     services.AddScoped<IConstructionCompanyAdapter , ConstructionCompanyAdapter>();
     //services.AddScoped<IConstructionCompanyRepository , ConstructionCompanyRepository>();
      
     services.AddScoped<IOwnerAdapter , OwnerAdapter>();
     //services.AddScoped<IOwnerRepository, OwnerRepository>();
      
     services.AddScoped<IBuildingAdapter , BuildingAdapter>();
     //services.AddScoped<IBuildingRepository , BuildingRepository>();
      
     services.AddScoped<IFlatAdapter , FlatAdapter>();
     //services.AddScoped<IFlatRepository , FlatRepository>();
      
     services.AddScoped<IMaintenanceRequestAdapter , MaintenanceRequestAdapter>();
     //services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
     
     services.AddScoped<IRequestHandlerAdapter , RequestHandlerAdapter>();
     //services.AddScoped<IRequestHandlerRepository, RequestHandlerRepository>();
      
     services.AddScoped<IManagerAdapter , ManagerAdapter>();
     //services.AddScoped<IManagerRepository, ManagerRepository>();

     services.AddScoped<IReportAdapter , ReportAdapter>();
     //services.AddScoped<IReportRepository, ReportRepository>();

     services.AddScoped<IAdministratorAdapter, AdministratorAdapter>();
     //services.AddScoped<IAdministratorRepository, AdministratorRepository>();
     
      return services;
   }
}