using Domain;
using IRepository;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class OwnerServiceTest
{
    #region Initialize

    private Mock<IOwnerRepository> _ownerRepository;
    private OwnerService _ownerService;

    [TestInitialize]
    public void Initialize()
    {
        _ownerRepository = new Mock<IOwnerRepository>(MockBehavior.Strict);
        _ownerService = new OwnerService(_ownerRepository.Object);
    }

    #endregion

    #region Get All Owners

    //Happy path
    [TestMethod]
    public void GetAllOwners_OwnersAreReturn()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "owner@gmail.com"
        };

        Flat flatOfOwner = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = ownerInDb,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        ownerInDb.Flats = new List<Flat> { flatOfOwner };

        IEnumerable<Owner> ownersInDb = new List<Owner>
        {
            ownerInDb
        };

        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(ownersInDb);

        IEnumerable<Owner> ownersResponse = _ownerService.GetAllOwners();
        _ownerRepository.VerifyAll();

        Assert.AreEqual(ownersInDb.Count(), ownersResponse.Count());
        Assert.IsTrue(ownersInDb.SequenceEqual(ownersResponse));
    }

    #region Get all Owners, Repository Validations

    [TestMethod]
    public void GetAllOwners_ThrowsUnknownErrorServiceException()
    {
        _ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners())
            .Throws(new UnknownRepositoryException("Unknown error in repository layer."));

        Assert.ThrowsException<UnknownServiceException>(() => _ownerService.GetAllOwners());
        _ownerRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Get Owner By Id

    [TestMethod]
    public void GetOwnerById_OwnerIsReturned()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "owner@gmail.com"
        };

        Flat flatOfOwner = new Flat
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = ownerInDb,
            TotalRooms = 3,
            TotalBaths = 2,
            HasTerrace = false
        };

        ownerInDb.Flats = new List<Flat> { flatOfOwner };


        _ownerRepository.Setup(ownerRepository => ownerRepository.GetOwnerById(It.IsAny<Guid>())).Returns(ownerInDb);
        
        Owner ownerObtained = _ownerService.GetOwnerById(ownerInDb.Id);
        
        Assert.IsTrue(ownerInDb.Equals(ownerObtained));
        
        _ownerRepository.VerifyAll();
    }

    #endregion
}