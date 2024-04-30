using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace Test.Adapters;

[TestClass]
public class ReportAdapterTest
{
    #region Initialize
    
    private Mock<IReportService> _reportService;
    private ReportAdapter _reportAdapter;
    private Guid _sampleBuildingId; 
    private Guid _sampleRequestHandlerId;
    private Guid _sampleCategoryId;
    
    [TestInitialize]
    public void Initialize()
    {
        _reportService = new Mock<IReportService>(MockBehavior.Strict);
        _reportAdapter = new ReportAdapter(_reportService.Object);
        _sampleBuildingId = Guid.NewGuid();
        _sampleRequestHandlerId = Guid.NewGuid();
        _sampleCategoryId = Guid.NewGuid();
    }

    #endregion
    
    #region Get Maintenance Reports By Building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByBuildingResponse> expectedAdapterResponse = new List<GetMaintenanceReportByBuildingResponse>()
        {
            new GetMaintenanceReportByBuildingResponse()
            {
                BuildingId = _sampleBuildingId,
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().BuildingId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByBuilding(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByBuildingResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByBuilding(_sampleBuildingId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetMaintenanceReportByBuilding(It.IsAny<Guid>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>()));
        
        _reportService.VerifyAll();
    }
    #endregion

    #region Get Maintenance Reports By Request Handler

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReturnsGetMaintenanceReportResponse()
    {
        
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedAdapterResponse = new List<GetMaintenanceReportByRequestHandlerResponse>()
        {
            new GetMaintenanceReportByRequestHandlerResponse
            {
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 0,
                AverageTimeToCloseRequest = 4
            }
        };
        IEnumerable<RequestHandlerReport> expectedServiceResponse = new List<RequestHandlerReport>()
        {
            new RequestHandlerReport
            {
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
                AvgTimeToCloseRequest = expectedAdapterResponse.First().AverageTimeToCloseRequest
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByRequestHandler(_sampleRequestHandlerId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>())).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()));
        
        _reportService.VerifyAll();
    }
    #endregion
    
    #region Get Maintenance Reports By Category 
    
    [TestMethod]
    public void GetMaintenanceReportByCategory_ReturnsGetMaintenanceReportResponse()
    {
        
        
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedAdapterResponse = new List<GetMaintenanceReportByCategoryResponse>()
        {
            new GetMaintenanceReportByCategoryResponse()
            {
                CategoryId = _sampleCategoryId,
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().CategoryId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByCategory(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByCategoryResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByCategory(_sampleCategoryId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByCategory_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetMaintenanceReportByCategory(It.IsAny<Guid>())).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetMaintenanceReportByCategory(It.IsAny<Guid>()));
        
        _reportService.VerifyAll();
    }
    
    #endregion

    #region Get All Maintenance Requests By Building
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByBuilding_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByBuildingResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByBuildingResponse>()
            {
                new GetMaintenanceReportByBuildingResponse()
                {
                    BuildingId = _sampleBuildingId,
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8
                },
                new GetMaintenanceReportByBuildingResponse()
                {
                    BuildingId = Guid.NewGuid(),
                    OpenRequests = 20,
                    ClosedRequests = 15,
                    OnAttendanceRequests = 18
                }
            };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {  
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().BuildingId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            },
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.Last().BuildingId,
                OpenRequests = expectedAdapterResponse.Last().OpenRequests,
                ClosedRequests = expectedAdapterResponse.Last().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.Last().OnAttendanceRequests
            }
        };
        
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByBuilding()).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByBuildingResponse> adapterResponse =
            _reportAdapter.GetAllBuildingMaintenanceReports();
        
        _reportService.VerifyAll();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByBuilding_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByBuilding()).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetAllBuildingMaintenanceReports());
        
        _reportService.VerifyAll();
    }
    

    #endregion
    
    #region Get All Maintenance Requests By Request Handler
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByRequestHandler_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByRequestHandlerResponse>()
            {
                new GetMaintenanceReportByRequestHandlerResponse()
                {
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8,
                    AverageTimeToCloseRequest = 4
                },
                new GetMaintenanceReportByRequestHandlerResponse()
                {
                    OpenRequests = 20,
                    ClosedRequests = 15,
                    OnAttendanceRequests = 18,
                    AverageTimeToCloseRequest = 6
                }
            };
        IEnumerable<RequestHandlerReport> expectedServiceResponse = new List<RequestHandlerReport>()
        {
            new RequestHandlerReport
            {
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
                AvgTimeToCloseRequest = expectedAdapterResponse.First().AverageTimeToCloseRequest
            },
            new RequestHandlerReport
            {
                OpenRequests = expectedAdapterResponse.Last().OpenRequests,
                ClosedRequests = expectedAdapterResponse.Last().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.Last().OnAttendanceRequests,
                AvgTimeToCloseRequest = expectedAdapterResponse.Last().AverageTimeToCloseRequest
            }
        };
        
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByRequestHandler()).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> adapterResponse =
            _reportAdapter.GetAllMaintenanceRequestsByRequestHandler();
        
        _reportService.VerifyAll();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByRequestHandler_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByRequestHandler()).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetAllMaintenanceRequestsByRequestHandler());
        
        _reportService.VerifyAll();
    }
    
    #endregion
    
    #region Get All Maintenance Requests By Category
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByCategory_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByCategoryResponse>()
            {
                new GetMaintenanceReportByCategoryResponse()
                {
                    CategoryId = _sampleCategoryId,
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8
                },
                new GetMaintenanceReportByCategoryResponse()
                {
                    CategoryId = Guid.NewGuid(),
                    OpenRequests = 20,
                    ClosedRequests = 15,
                    OnAttendanceRequests = 18
                }
            };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().CategoryId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            },
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.Last().CategoryId,
                OpenRequests = expectedAdapterResponse.Last().OpenRequests,
                ClosedRequests = expectedAdapterResponse.Last().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.Last().OnAttendanceRequests
            }
        };
        
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByCategory()).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByCategoryResponse> adapterResponse =
            _reportAdapter.GetAllMaintenanceRequestsByCategory();
        
        _reportService.VerifyAll();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequestsByCategory_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetAllMaintenanceRequestsByCategory()).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetAllMaintenanceRequestsByCategory());
        
        _reportService.VerifyAll();
    }

    
    #endregion
}