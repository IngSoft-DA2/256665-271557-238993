using Domain;
using IRepository;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class ConstructionCompanyServiceTest
{
    #region TestInitialize

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
    public void CreateConstructionCompanyWithUsedName_ThrowsRepeatedObjectErrorException()
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
            constructionCompanyRepository.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Throws(new Exception());
        _constructionCompanyRepository.Setup(constructionCompanyRepository =>
            constructionCompanyRepository.GetAllConstructionCompanies()).Returns(new List<ConstructionCompany>());
        
        
        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyService.CreateConstructionCompany(constructionCompanyToAdd));
        
        _constructionCompanyRepository.VerifyAll();
    }
    
    
    
    
    #endregion

    #endregion
}