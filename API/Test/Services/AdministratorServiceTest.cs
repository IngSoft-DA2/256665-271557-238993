using Domain;
using IRepository;
using IServiceLogic;
using Moq;
using ServiceLogic;

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
}