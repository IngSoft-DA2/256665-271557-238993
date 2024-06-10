using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class AdministratorRepositoryTest
{
    private DbContext _dbContext;
    private AdministratorRepository _administratorRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("AdministratorRepositoryTest");
        _dbContext.Set<Administrator>();
        _administratorRepository = new AdministratorRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllAdministrator_ReturnsAllAdministrators()
    {
        IEnumerable<Administrator> administratorsInDb = new List<Administrator>
        {
            new Administrator
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Admin,
                Firstname = "Administrator1",
                Lastname = "Administrator1",
                Email = "administrator@gmail.com",
                Password = "password",
                Invitations = new List<Invitation>
                {
                    new Invitation
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Invitation1",
                        Lastname = "Invitation1",
                        Email = "invitation@example.com",
                        ExpirationDate = DateTime.MaxValue,
                        Status = StatusEnum.Pending
                    }
                }
            },
            new Administrator
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Admin,
                Firstname = "Administrator2",
                Lastname = "Administrator2",
                Email = "administrato2@gmail.com",
                Password = "password2",
            }
        };

        _dbContext.Set<Administrator>().Add(administratorsInDb.ElementAt(0));
        _dbContext.Set<Administrator>().Add(administratorsInDb.ElementAt(1));
        _dbContext.SaveChanges();

        IEnumerable<Administrator> administratorsResponse = _administratorRepository.GetAllAdministrators();

        Assert.IsTrue(administratorsInDb.SequenceEqual(administratorsResponse));
    }

    [TestMethod]
    public void GetAllAdministrators_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Administrator>()).Throws(new Exception());

        _administratorRepository = new AdministratorRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _administratorRepository.GetAllAdministrators());
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void CreateAdministrator_AdministratorIsCreated()
    {
        Administrator administratorToAdd = new Administrator
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Admin,
            Firstname = "Administrator1",
            Lastname = "Administrator1",
            Email = "admin@gmail.com",
            Password = "password",
            Invitations = new List<Invitation>()
        };

        _administratorRepository.CreateAdministrator(administratorToAdd);

        Administrator administratorInDb = _dbContext.Set<Administrator>().Find(administratorToAdd.Id);

        Assert.AreEqual(administratorToAdd, administratorInDb);
    }

    [TestMethod]
    public void CreateAdministrator_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Administrator>()).Throws(new Exception());

        _administratorRepository = new AdministratorRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _administratorRepository.CreateAdministrator(new Administrator()));
        _mockDbContext.VerifyAll();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}