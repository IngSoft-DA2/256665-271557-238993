using Domain;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class ReportServiceTest
{

    #region Test initialize
    
    private Mock<IReportRepository> _reportRepository;
    private ReportService _reportService;
    
    [TestInitialize]
    public void Initialize()
    {
        _reportRepository = new Mock<IReportRepository>(MockBehavior.Strict);
        _reportService = new ReportService(_reportRepository.Object);
    }

    #endregion
    
    
    #region Get maintenance report by building
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReportsAreReturned()
    {
        
        IEnumerable<Report> expectedRepositoryResponse = new List<Report>
        {
            new Report()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OnAttendanceRequests = 1,
                OpenRequests = 23,
                ClosedRequests = 10,
                
            },
            new Report()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OnAttendanceRequests = 2,
                OpenRequests = 20,
                ClosedRequests = 12,
            }
        };
        
        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
            .Returns(expectedRepositoryResponse);
        
        IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>());
        
        _reportRepository.VerifyAll();
        
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_NoReportsAreReturned()
    {
        
        IEnumerable<Report> expectedRepositoryResponse = new List<Report>();
        
        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
            .Returns(expectedRepositoryResponse);
        
        IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>());
        
        _reportRepository.VerifyAll();
        
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    }
    
    #endregion

    
}