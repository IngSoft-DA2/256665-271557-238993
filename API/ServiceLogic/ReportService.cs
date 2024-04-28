using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ReportService
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
    
    

}