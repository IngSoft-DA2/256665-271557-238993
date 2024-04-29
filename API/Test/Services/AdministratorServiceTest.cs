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
    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdministrator()
    {
        Administrator administrator = new Administrator
        {
            Id = Guid.NewGuid(),
            Firstname = "Administrator",
            LastName = "AdministratorLastName",
            Email = "person@gmail.com",
            Password = "12345678"
        };

        Mock<IAdministratorRepository> administratorRepository =
            new Mock<IAdministratorRepository>(MockBehavior.Strict);
        administratorRepository.Setup(repo => repo.CreateAdministrator(administrator));

        AdministratorService administratorService = new AdministratorService(administratorRepository.Object);

        administratorService.CreateAdministrator(administrator);

        Assert.IsNotNull(administrator);

        administratorRepository.VerifyAll();
    }

    [TestMethod]
    public void GivenEmptyEmailOnCreate_ShouldThrowObjectErrorServiceException()
    {
        Administrator administrator = new Administrator
        {
            Id = Guid.NewGuid(),
            Firstname = "Administrator",
            LastName = "AdministratorLastName",
            Email = "",
            Password = "12345678"
        };
        
        Mock<IAdministratorRepository> administratorRepository =
            new Mock<IAdministratorRepository>(MockBehavior.Strict);
        
        administratorRepository.Setup(repo => repo.CreateAdministrator(administrator)).Throws(new InvalidPersonException("Email is required"));
        
        AdministratorService administratorService = new AdministratorService(administratorRepository.Object);
        
        Assert.ThrowsException<ObjectErrorServiceException>(() => administratorService.CreateAdministrator(administrator));
    }

    [TestMethod]
    public void GivenEmptyPasswordOnCreate_ShouldThrowObjectErrorServiceException()
    {
        Administrator administrator = new Administrator
        {
            Id = Guid.NewGuid(),
            Firstname = "Administrator",
            LastName = "AdministratorLastName",
            Email = "person@email.com",
            Password = "",
        };

        Mock<IAdministratorRepository> administratorRepository =
            new Mock<IAdministratorRepository>(MockBehavior.Strict);

        administratorRepository.Setup(repo => repo.CreateAdministrator(administrator))
            .Throws(new InvalidManagerException("Password must have at least 8 characters"));

        AdministratorService administratorService = new AdministratorService(administratorRepository.Object);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            administratorService.CreateAdministrator(administrator));

    }
}