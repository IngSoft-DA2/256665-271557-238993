using Domain;
using IServiceLogic;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class FlatServiceTest
{
    #region Initialize

    private FlatService _flatService;
    private Mock<IFlatRepository> _flatRepository;

    [TestInitialize]
    public void Initialize()
    {
        _flatRepository = new Mock<IFlatRepository>(MockBehavior.Strict);
        _flatService = new FlatService(_flatRepository.Object);
    }

    #endregion
    
    #region Get all Flats
    //Happy Path
    [TestMethod]
    public void GetAllFlats_FlatsAreReturn()
    {
        Owner ownerOfFlatInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "owner@gmail.com",
        };

        Flat flatExampleInDb = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = ownerOfFlatInDb,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        ownerOfFlatInDb.Flats = new List<Flat> { flatExampleInDb };

        IEnumerable<Flat> flatsInDb = new List<Flat> { flatExampleInDb };

        _flatRepository.Setup(flatRepository => flatRepository.GetAllFlats()).Returns(flatsInDb);

        IEnumerable<Flat> flats = _flatService.GetAllFlats();
        _flatRepository.VerifyAll();

        Assert.IsTrue(flats.SequenceEqual(flatsInDb));
    }

    #region Get all Flats, Repository Validations

    [TestMethod]
    public void GetAllFlats_ThrowsUnknownErrorServiceException()
    {
        _flatRepository.Setup(flatRepository => flatRepository.GetAllFlats())
            .Throws(new UnknownRepositoryException("Unknown error"));
        Assert.ThrowsException<UnknownServiceException>(() => _flatService.GetAllFlats());
        _flatRepository.VerifyAll();
    }

    #endregion

    #endregion
    
    
    
    
}