﻿using Adapter;
using Adapter.CustomExceptions;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.AdministratorResponses;

namespace Test.Adapters;

[TestClass]
public class AdministratorAdapterTest
{
    #region Initialize
    
    private Mock<IAdministratorService> _administratorService;
    private AdministratorAdapter _administratorAdapter;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _administratorService = new Mock<IAdministratorService>(MockBehavior.Strict);
        _administratorAdapter = new AdministratorAdapter(_administratorService.Object);
    }
    
    #endregion
    
    #region Create Administrator
    
    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdminsitrator()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(administratorId));
        
        CreateAdministratorResponse response = _administratorAdapter.CreateAdministrator(administratorId);
        
        Assert.AreEqual(administratorId, response.Id);
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowObjectRepeatedAdapterException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(administratorId)).Throws(new ObjectRepeatedServiceException());
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<ObjectRepeatedAdapterException>(() => administratorAdapter.CreateAdministrator(administratorId));
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowObjectErrorAdapterException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(administratorId)).Throws(new ObjectErrorServiceException("Error creating administrator"));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() => administratorAdapter.CreateAdministrator(administratorId));
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(administratorId)).Throws(new Exception("Error creating administrator"));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<Exception>(() => administratorAdapter.CreateAdministrator(administratorId));
    }
    
    #endregion
}