using Domain;
using IDataAccess;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class ConstructionCompanyServiceTest
{
    #region Initialize

    private Mock<IConstructionCompanyRepository> _constructionCompanyRepository;
    private ConstructionCompanyService _constructionCompanyService;

    [TestInitialize]
    public void TestInitialize()
    {
        _constructionCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
        _constructionCompanyService = new ConstructionCompanyService(_constructionCompanyRepository.Object);
    }

    #endregion

    #region Get All Construction Companies

    //Happy Path
    [TestMethod]
    public void GetAllConstructionCompanies_ConstructionCompaniesAreReturn()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1"
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2"
            }
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(constructionCompaniesInDb);

        IEnumerable<ConstructionCompany> constructionCompaniesObtained =
            _constructionCompanyService.GetAllConstructionCompanies();

        _constructionCompanyRepository.VerifyAll();

        Assert.AreEqual(constructionCompaniesInDb.Count(), constructionCompaniesObtained.Count());
        Assert.IsTrue(constructionCompaniesInDb.SequenceEqual(constructionCompaniesObtained));
    }

    #region Get All Construction Companies, Repository Validations

    [TestMethod]
    public void GetAllConstructionCompanies_UnknownServiceExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetAllConstructionCompanies())
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.GetAllConstructionCompanies());
        _constructionCompanyRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Get Construction Company By Id

    //Happy path
    [TestMethod]
    public void GetConstructionCompanyById_ReturnsConstructionCompany()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company 1"
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(constructionCompanyInDb);

        ConstructionCompany constructionCompanyObtained =
            _constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>());

        _constructionCompanyRepository.VerifyAll();

        Assert.AreEqual(constructionCompanyInDb, constructionCompanyObtained);
    }

    #region Get Construction Company By Id, Repository Validations

    [TestMethod]
    public void GetConstructionCompanyById_ObjectNotFoundServiceExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()));
        _constructionCompanyRepository.VerifyAll();
    }

    [TestMethod]
    public void GetConstructionCompanyById_UnknownServiceExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()));
        _constructionCompanyRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Get Construction Company By User Creator Id

    //happy path
    [TestMethod]
    public void GetConstructionCompanyByUserCreatorId_ReturnsConstructionCompany()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company 1",
            UserCreatorId = Guid.NewGuid(),
            Buildings = new List<Building>()
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>()))
            .Returns(constructionCompanyInDb);

        ConstructionCompany constructionCompanyObtained =
            _constructionCompanyService.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>());

        _constructionCompanyRepository.VerifyAll();

        Assert.AreEqual(constructionCompanyInDb, constructionCompanyObtained);
    }
    
    #region Get Construction Company By User Creator Id, Repository Validations

    [TestMethod]
    public void GetConstructionCompanyByUserCreatorId_ObjectNotFoundServiceExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _constructionCompanyService.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>()));
        _constructionCompanyRepository.VerifyAll();
    }
    
    [TestMethod]
    public void GetConstructionCompanyByUserCreatorId_UnknownServiceExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.GetConstructionCompanyByUserCreatorId(It.IsAny<Guid>()));
        _constructionCompanyRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Create Construction Company

    //Happy path
    [TestMethod]
    public void CreateConstructionCompany_ConstructionCompanyIsCreated()
    {
        ConstructionCompany constructionCompanyToAdd = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company1"
        };

        _constructionCompanyRepository.Setup(constructionCompany =>
            constructionCompany.CreateConstructionCompany(It.IsAny<ConstructionCompany>()));
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(new List<ConstructionCompany>());

        _constructionCompanyService.CreateConstructionCompany(constructionCompanyToAdd);

        _constructionCompanyRepository.VerifyAll();
    }

    #region Create Construction Company, Domain Validations

    [TestMethod]
    public void CreateConstructionCompanyWithEmptyName_ThrowsObjectErrorServiceException()
    {
        ConstructionCompany constructionCompanyWithError = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = ""
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyWithError));
    }

    [TestMethod]
    public void CreateConstructionCompanyWithNameGreaterThan100Chars_ThrowsObjectErrorServiceException()
    {
        ConstructionCompany constructionCompanyWithError = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "C".PadRight(101, 'y')
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyWithError));
    }

    #endregion

    #region Create Construction Company, Repository Validations

    [TestMethod]
    public void CreateConstructionCompanyWithUsedName_ThrowsRepeatedObjectRepeatedServiceException()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1"
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2"
            }
        };

        ConstructionCompany constructionCompanyToAdd = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company 1"
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(constructionCompaniesInDb);

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyToAdd));

        _constructionCompanyRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompany_UnknownServiceExceptionIsThrown()
    {
        ConstructionCompany constructionCompanyToAdd = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company 1"
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.CreateConstructionCompany(It.IsAny<ConstructionCompany>()))
            .Throws(new Exception());
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(new List<ConstructionCompany>());


        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyToAdd));

        _constructionCompanyRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyWithUserWhoHasCreatedOneBefore_ShouldThrowObjectErrorServiceException()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1",
                UserCreatorId = Guid.NewGuid()
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2",
                UserCreatorId = Guid.NewGuid()
            }
        };

        ConstructionCompany constructionCompanyToAdd = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Company 3",
            UserCreatorId = constructionCompaniesInDb.First().UserCreatorId
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(constructionCompaniesInDb);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyToAdd));

        _constructionCompanyRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Update Construction Company

    //happy path
    [TestMethod]
    public void UpdateConstructionCompany_ConstructionCompanyIsUpdated()
    {
        ConstructionCompany constructionCompanyWithoutUpdates = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "CompanyNotUpdated",
            UserCreatorId = Guid.NewGuid()
        };

        ConstructionCompany constructionCompanyToUpdate = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "NameUpdated",
        };


        _constructionCompanyRepository.Setup(constructionCompany =>
                constructionCompany.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(constructionCompanyWithoutUpdates);

        _constructionCompanyRepository.Setup(constructionCompany =>
            constructionCompany.UpdateConstructionCompany(It.IsAny<ConstructionCompany>()));

        _constructionCompanyRepository.Setup(constructionCompany =>
            constructionCompany.GetAllConstructionCompanies()).Returns(new List<ConstructionCompany>
            { constructionCompanyWithoutUpdates });


        _constructionCompanyService.UpdateConstructionCompany(constructionCompanyToUpdate);

        _constructionCompanyRepository.VerifyAll();
    }


    #region Update Construction Company, Domain Validations

    [TestMethod]
    public void UpdateConstructionCompanyWithWrongFormatName_ThrowsObjectErrorServiceException()
    {
        ConstructionCompany constructionCompanyWithoutUpdates = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company",
            UserCreatorId = Guid.NewGuid()
        };


        ConstructionCompany constructionCompanyWithError = new ConstructionCompany
        {
            Id = constructionCompanyWithoutUpdates.Id,
            Name = "",
            UserCreatorId = constructionCompanyWithoutUpdates.UserCreatorId
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetAllConstructionCompanies())
            .Returns(new List<ConstructionCompany> { constructionCompanyWithoutUpdates });

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(constructionCompanyWithoutUpdates);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyService.UpdateConstructionCompany(constructionCompanyWithError));

        _constructionCompanyRepository.VerifyAll();
    }

    #endregion

    #region Update Construction Company, Repository Validations

    [TestMethod]
    public void UpdateConstructionCompanyWithUsedName_ThrowsObjectErrorServiceException()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1"
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2"
            }
        };

        ConstructionCompany constructionCompanyWithUpdates = new ConstructionCompany
        {
            Id = constructionCompaniesInDb.First().Id,
            Name = "Company 1"
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(constructionCompaniesInDb);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyService.UpdateConstructionCompany(constructionCompanyWithUpdates));

        _constructionCompanyRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateConstructionCompany_ObjectNotFoundServiceExceptionIsThrown()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1",
                UserCreatorId = Guid.NewGuid()
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2",
                UserCreatorId = Guid.NewGuid()
            }
        };

        ConstructionCompany constructionCompanyWithUpdates = new ConstructionCompany
        {
            Id = constructionCompaniesInDb.First().Id,
            Name = "New name"
        };

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(constructionCompaniesInDb);

        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _constructionCompanyService.UpdateConstructionCompany(constructionCompanyWithUpdates));

        _constructionCompanyRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateConstructionCompany_UnknownExceptionIsThrown()
    {
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetConstructionCompanyById(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.UpdateConstructionCompany(new ConstructionCompany()));
    }

    #endregion

    #endregion
}