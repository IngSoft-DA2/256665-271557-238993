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
    public void GetAllFlats_BuildingIsNotFound_ThrowsObjectNotFoundServiceException()
    {
        _flatRepository.Setup(flatRepository => flatRepository.GetAllFlats(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _flatService.GetAllFlats(It.IsAny<Guid>()));
        _flatRepository.VerifyAll();
    }
    
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

        Assert.ThrowsException<UnknownServiceException>(() =>
            _flatService.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()));
        _flatRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Create Flat

    //Happy Path
    [TestMethod]
    public void CreateFlat_FlatIsCreated()
    {
        Flat flatToAdd = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 101,
            OwnerAssigned = new Owner(),
            TotalRooms = 2,
            TotalBaths = 1,
            HasTerrace = true
        };

        _flatRepository.Setup(flatRepository => flatRepository.CreateFlat(flatToAdd));

        _flatService.CreateFlat(flatToAdd);
        _flatRepository.VerifyAll();
    }

    #region Create Flat, Domain Validations

    [TestMethod]
    public void CreateFlatWithNoTotalRooms_ThrowsObjectErrorServiceException()
    {
        Flat flatWithNoRooms = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 101,
            OwnerAssigned = new Owner(),
            TotalRooms = 0,
            TotalBaths = 1,
            HasTerrace = true
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _flatService.CreateFlat(flatWithNoRooms));
    }
    
    [TestMethod]
    public void CreateFlatWithNegativeTotalBaths_ThrowsObjectErrorServiceException()
    {
        Flat flatWithNegativeRooms = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 101,
            OwnerAssigned = new Owner(),
            TotalRooms = 1,
            TotalBaths = -1,
            HasTerrace = true
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _flatService.CreateFlat(flatWithNegativeRooms));
    }

    #endregion

    #region Create Flat, Repository Validations

    [TestMethod]    
    public void CreateFlat_ThrowsUnknownErrorServiceException()
    {

        Flat flatToAdd = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 101,
            OwnerAssigned = new Owner(),
            TotalRooms = 1,
            TotalBaths = 1,
            HasTerrace = true
        };

        _flatRepository.Setup(flatRepository => flatRepository.CreateFlat(flatToAdd))
            .Throws(new UnknownRepositoryException("Unknown error"));

        Assert.ThrowsException<UnknownServiceException>(() => _flatService.CreateFlat(flatToAdd));
        _flatRepository.VerifyAll();
    }
    

    #endregion

    #endregion
}