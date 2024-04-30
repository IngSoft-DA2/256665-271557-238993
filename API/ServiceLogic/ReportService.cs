using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ReportService : IReportService
{
    #region Constructor and Dependency Injection
    
    private readonly IReportRepository _reportRepository;
    
    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    
    #endregion
    
    #region Get maintenance report by building
    public IEnumerable<Report> GetMaintenanceReportByBuilding(Guid buildingId)
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetMaintenanceReportByBuilding(buildingId);
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
        
    }
    
    #endregion
    
    
    #region Get maintenance report by request handler
    public IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid isAny)
    {
        try
        {
            IEnumerable<RequestHandlerReport> reports = _reportRepository.GetMaintenanceReportByRequestHandler(isAny);
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get maintenance report by category
    public IEnumerable<Report> GetMaintenanceReportByCategory(Guid isAny)
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetMaintenanceReportByCategory(isAny);
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<Report> GetAllMaintenanceRequestsByBuilding()
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetAllMaintenanceRequestsByBuilding();
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<RequestHandlerReport> GetAllMaintenanceRequestsByRequestHandler()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Report> GetAllMaintenanceRequestsByCategory()
    {
        throw new NotImplementedException();
    }

    #endregion
}