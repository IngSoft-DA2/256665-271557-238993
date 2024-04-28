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

        Mock<IOwnerRepository> ownerRepository = new Mock<IOwnerRepository>(MockBehavior.Strict);
        ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners()).Returns(ownersInDb);

        OwnerService ownerService = new OwnerService(ownerRepository.Object);

        IEnumerable<Owner> ownersResponse = ownerService.GetAllOwners();
        ownerRepository.VerifyAll();

        Assert.AreEqual(ownersInDb.Count(), ownersResponse.Count());
        Assert.IsTrue(ownersInDb.SequenceEqual(ownersResponse));
    }

    #region Get all Owners, Repository Validations

    [TestMethod]
    public void GetAllOwners_ThrowsUnknownErrorServiceException()
    {
        Mock<IOwnerRepository> ownerRepository = new Mock<IOwnerRepository>(MockBehavior.Strict);
        ownerRepository.Setup(ownerRepository => ownerRepository.GetAllOwners())
            .Throws(new UnknownRepositoryException("Unknown error in repository layer."));

        OwnerService ownerService = new OwnerService(ownerRepository.Object);

        Assert.ThrowsException<UnknownServiceException>(() => ownerService.GetAllOwners());
        ownerRepository.VerifyAll();
    }

    #endregion

    #endregion
}