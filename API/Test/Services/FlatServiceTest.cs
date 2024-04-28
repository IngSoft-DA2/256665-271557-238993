using Domain;
using IRepository;
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
        Building dummyBuilding = new Building();
        Owner dummyOwner = new Owner();

        Flat flatExampleInDb = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = dummyBuilding.Id,
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = dummyOwner,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        dummyOwner.Flats = new List<Flat> { flatExampleInDb };

        IEnumerable<Flat> flatsInDb = new List<Flat> { flatExampleInDb };

        _flatRepository.Setup(flatRepository => flatRepository.GetAllFlats(It.IsAny<Guid>())).Returns(flatsInDb);

        IEnumerable<Flat> flats = _flatService.GetAllFlats(dummyBuilding.Id);
        _flatRepository.VerifyAll();

        Assert.IsTrue(flats.SequenceEqual(flatsInDb));
    }

    #region Get all Flats, Repository Validations

    [TestMethod]
    public void GetAllFlats_ThrowsUnknownErrorServiceException()
    {
        _flatRepository.Setup(flatRepository => flatRepository.GetAllFlats(It.IsAny<Guid>()))
            .Throws(new UnknownRepositoryException("Unknown error"));

        Assert.ThrowsException<UnknownServiceException>(() => _flatService.GetAllFlats(It.IsAny<Guid>()));
        _flatRepository.VerifyAll();
    }

    #endregion

    #endregion


    #region Get Flat By Id

    //Happy Path
    [TestMethod]
    public void GetFlatById_FlatIsReturn()
    {
        Owner dummyOwner = new Owner();
        Building dummyBuilding = new Building();

        Flat flatInDb = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = dummyBuilding.Id,
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = dummyOwner,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };


        _flatRepository.Setup(flatRepository =>
            flatRepository.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(flatInDb);

        Flat flatFound = _flatService.GetFlatById(dummyBuilding.Id, flatInDb.Id);
        Assert.IsTrue(flatInDb.Equals(flatFound));
        _flatRepository.VerifyAll();
    }

    #region Get Flat By Id, Repository Validations

    [TestMethod]
    public void GetFlatById_FlatIsNotFound()
    {
        _flatRepository.Setup(flatRepository =>
            flatRepository.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _flatService.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()));
        _flatRepository.VerifyAll();
    }
    
    [TestMethod]
    public void GetFlatById_ThrowsUnknownErrorServiceException()
    {
        _flatRepository.Setup(flatRepository => flatRepository.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new UnknownRepositoryException("Unknown error"));

        Assert.ThrowsException<UnknownServiceException>(() => _flatService.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()));
        _flatRepository.VerifyAll();
    }

    #endregion

    #endregion
}