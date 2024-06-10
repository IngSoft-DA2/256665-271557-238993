using Adapter;
using Adapter.CustomExceptions;
using Domain;
using ILoaders;
using IServiceLogic;
using Moq;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace Test.Adapters;

[TestClass]
public class LoaderAdapterTest
{
    private Mock<ILoaderService> _mockService;
    private Mock<ILoader> _mockLoader;
    private Mock<ISessionService> _mockSessionService;
    private Mock<IBuildingService> _mockBuildingService;
    private Mock<IConstructionCompanyService> _mockConstructionCompanyService;
    private Mock<IManagerService> _mockManagerService;
    private Mock<IOwnerService> _mockOwnerService;
    private LoaderAdapter _loaderAdapter;
    

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<ILoaderService>(behavior: MockBehavior.Strict);
        _mockLoader = new Mock<ILoader>(behavior: MockBehavior.Strict);
        _mockSessionService = new Mock<ISessionService>(behavior: MockBehavior.Strict);
        _mockBuildingService = new Mock<IBuildingService>(behavior: MockBehavior.Strict);
        _mockConstructionCompanyService = new Mock<IConstructionCompanyService>(behavior: MockBehavior.Strict);
        _mockManagerService = new Mock<IManagerService>(behavior: MockBehavior.Strict);
        _mockOwnerService = new Mock<IOwnerService>(behavior: MockBehavior.Strict);
        
    }

    [TestMethod]
    public void CreateAllBuildingsFromLoad_ReturnsListOfCreateBuildingFromLoadResponse()
    {
        string filePath = "test/path/to/file.xml";
        Guid sessionStringOfUser = Guid.NewGuid();
        
        ImportBuildingFromFileRequest importBuildingFromFileRequest = new ImportBuildingFromFileRequest
        {
            FilePath = filePath
        };

        List<CreateBuildingFromLoadResponse> createBuildingFromLoadResponses = new List<CreateBuildingFromLoadResponse>
        {
            new CreateBuildingFromLoadResponse
            {
                Details = "test",
                idOfBuildingCreated = Guid.NewGuid()
            }
        };

        _mockService.Setup(service => service.GetAllLoaders()).Returns(new List<ILoader> { _mockLoader.Object });
        _mockSessionService.Setup(service => service.GetUserIdBySessionString(sessionStringOfUser)).Returns(Guid.NewGuid());
        _mockLoader.Setup(loader => loader.LoadAllBuildings(filePath)).Returns(new List<Building>());

        _loaderAdapter = new LoaderAdapter(_mockService.Object, _mockBuildingService.Object, _mockManagerService.Object, _mockOwnerService.Object, _mockSessionService.Object, _mockConstructionCompanyService.Object);

        List<CreateBuildingFromLoadResponse> result = _loaderAdapter.CreateAllBuildingsFromLoad(importBuildingFromFileRequest, sessionStringOfUser);

        Assert.IsInstanceOfType(result, typeof(List<CreateBuildingFromLoadResponse>));
        Assert.AreEqual(createBuildingFromLoadResponses.Count, result.Count);
        Assert.AreEqual(createBuildingFromLoadResponses[0].Details, result[0].Details);
        Assert.AreEqual(createBuildingFromLoadResponses[0].idOfBuildingCreated, result[0].idOfBuildingCreated);
        
        _mockService.Verify(service => service.GetAllLoaders(), Times.Once);
        _mockSessionService.Verify(service => service.GetUserIdBySessionString(sessionStringOfUser), Times.Once);
        _mockLoader.Verify(loader => loader.LoadAllBuildings(filePath), Times.Once);
        
    }
    
    // [TestMethod]
    // public void CreateAllBuildingsFromLoad_ThrowsUnknownAdapterException()
    // {
    //     string filePath = "test";
    //     
    //     ImportBuildingFromFileRequest importBuildingFromFileRequest = new ImportBuildingFromFileRequest
    //     {
    //         FilePath = filePath
    //     };
    //
    //     _mockService.Setup(service => service.GetAllLoaders()).Throws(new Exception("Error"));
    //
    //     Assert.ThrowsException<UnknownAdapterException>(() => _loaderAdapter.CreateAllBuildingsFromLoad(importBuildingFromFileRequest));
    //     
    //     _mockService.Verify(service => service.GetAllLoaders(), Times.Once);
    // }
    
    [TestMethod]
    public void GetAllLoaders_ReturnsListOfString()
    {
        string loaderName = "testLoader";
        
        _mockLoader.Setup(loader => loader.LoaderName()).Returns(loaderName);

        List<ILoader> loaders = new List<ILoader> { _mockLoader.Object };

        _mockService.Setup(service => service.GetAllLoaders()).Returns(loaders);

        List<string> result = _loaderAdapter.GetAllLoaders();   

        Assert.IsInstanceOfType(result, typeof(List<string>));
        Assert.AreEqual(loaders.Count, result.Count);
        Assert.AreEqual(loaderName, result[0]);
    }
    
    [TestMethod]
    public void GetAllLoaders_ThrowsUnknownAdapterException()
    {
        string filePath = "test";
        
        ImportBuildingFromFileRequest importBuildingFromFileRequest = new ImportBuildingFromFileRequest
        {
            FilePath = filePath
        };

        _mockService.Setup(service => service.GetAllLoaders()).Throws(new Exception("Error"));

        Assert.ThrowsException<UnknownAdapterException>(() => _loaderAdapter.GetAllLoaders());
        
        _mockService.Verify(service => service.GetAllLoaders(), Times.Once);
    }
}
