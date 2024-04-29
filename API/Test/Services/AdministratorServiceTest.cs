using Domain;
using IRepository;
using IServiceLogic;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class AdministratorServiceTest
{
    
    private AdministratorService _administratorService;
    private Mock<IAdministratorRepository> _administratorRepository;
    private Administrator _genericAdministrator;
    
    [TestInitialize]
    public void Setup()
    {
        _administratorRepository = new Mock<IAdministratorRepository>(MockBehavior.Strict);
        _administratorService = new AdministratorService(_administratorRepository.Object);
        
        _genericAdministrator = new Administrator
        {
            Id = Guid.NewGuid(),
            Firstname = "Administrator",
            LastName = "AdministratorLastName",
            Email = "person@gmail.com",
            Password = "12345678"
        };
        
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdministrator()
    {
        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator));
        _administratorRepository.Setup(repo => repo.GetAllAdministrators()).Returns(new List<Administrator>());

        _administratorService.CreateAdministrator(_genericAdministrator);

        Assert.IsNotNull(_genericAdministrator);

        _administratorRepository.VerifyAll();
    }

    [TestMethod]
    public void GivenEmptyEmailOnCreate_ShouldThrowObjectErrorServiceException()
    {
        _genericAdministrator.Email = "";
        
        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator)).Throws(new InvalidAdministratorException("Email is required"));
        
        Assert.ThrowsException<ObjectErrorServiceException>(() => _administratorService.CreateAdministrator(_genericAdministrator));
    }

    [TestMethod]
    public void GivenEmptyPasswordOnCreate_ShouldThrowObjectErrorServiceException()
    {
        _genericAdministrator.Password = "";

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new InvalidManagerException("Password must have at least 8 characters"));
        
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));

    }
    
    [TestMethod]
    public void GivenRepeatedEmailOnCreate_ShouldThrowObjectRepeatedServiceException()
    {
        _administratorRepository.Setup(repo => repo.GetAllAdministrators()).Returns(new List<Administrator>
        {
            _genericAdministrator
        });

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new ObjectRepeatedServiceException());
        
        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));
    }
    
    [TestMethod]
public void GivenNullLastNameOnCreate_ShouldThrowObjectErrorServiceException()
    {
        _genericAdministrator.LastName = null;

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new InvalidAdministratorException("Last name is required"));
        
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));
    }
    
}