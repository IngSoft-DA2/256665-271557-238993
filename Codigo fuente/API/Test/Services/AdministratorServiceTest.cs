using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class AdministratorServiceTest
{
    #region Initialize

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
            Lastname = "AdministratorLastName",
            Email = "person@gmail.com",
            Password = "12345678"
        };
    }

    #endregion

    #region Create Administrator

    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdministrator()
    {
        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator));
        _administratorRepository.Setup(repo => repo.GetAllAdministrators()).Returns(new List<Administrator>());

        _administratorService.CreateAdministrator(_genericAdministrator);

        Assert.IsNotNull(_genericAdministrator);
        Assert.IsNotNull(_genericAdministrator.Invitations);

        _administratorRepository.VerifyAll();
    }

    [TestMethod]
    public void GivenEmptyEmailOnCreate_ShouldThrowObjectErrorServiceException()
    {
        _genericAdministrator.Email = "";

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new InvalidAdministratorException("Email is required"));

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));
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
        _genericAdministrator.Lastname = null;

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new InvalidAdministratorException("Last name is required"));

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));
    }

    [TestMethod]
    public void GivenNullPasswordOnCreate_ShouldThrowObjectErrorServiceException()
    {
        _genericAdministrator.Password = null;

        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new InvalidAdministratorException("Password must have at least 8 characters"));

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));
    }

    [TestMethod]
    public void CreateAdministrator_ShouldThrowUnknownServiceException()
    {
        _administratorRepository.Setup(repo => repo.CreateAdministrator(_genericAdministrator))
            .Throws(new Exception("Unknown exception"));

        _administratorRepository.Setup(repo => repo.GetAllAdministrators()).Returns(new List<Administrator>());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _administratorService.CreateAdministrator(_genericAdministrator));

        _administratorRepository.VerifyAll();
    }

    #endregion
}