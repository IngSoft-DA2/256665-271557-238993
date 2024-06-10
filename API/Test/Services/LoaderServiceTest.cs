using System.Text;
using ILoaders;
using IServiceLogic;
using Microsoft.Extensions.Configuration;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class LoaderServiceTest
{
    
    #region Initialize
    
    private ILoaderService _loaderService;
    private Mock<IConfiguration> _configurationMock;
    private string _dllPath;
    
    #endregion
    
    
    #region TestInitialize
    
    [TestInitialize]
    public void TestInitialize()
    {
        _configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
        _configurationMock.Setup(x => x["AppSettings:LoadersPath"]).Returns("Loaders");
        _dllPath = Path.Combine(Directory.GetCurrentDirectory(), "Loaders/Loader.dll");
        _loaderService = new LoaderService(_configurationMock.Object);
        
    }
    
    #endregion

    [TestMethod]
    public void GetImporters_ReturnsImporterList()
    {
        
        // Arrange
        var importers = _loaderService.GetAllLoaders();
        
        // Assert
        Assert.IsNotNull(importers);
    }
}