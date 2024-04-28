

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


        Mock<IFlatRepository> flatRepository = new Mock<IFlatRepository>(MockBehavior.Strict);
        FlatService flatService = new FlatService(flatRepository.Object);
        
        
        flatRepository.Setup(flatRepository => flatRepository.GetAllFlats()).Returns(flatsInDb);
        
        IEnumerable<Flat> flats = flatService.GetAllFlats();
        flatRepository.VerifyAll();
        
        Assert.IsTrue(flats.SequenceEqual(flatsInDb));
        
    }
    
    [TestMethod]
    public void GetAllFlats_ThrowsUnknownErrorServiceException()
    {
        Mock<IFlatRepository> flatRepository = new Mock<IFlatRepository>(MockBehavior.Strict);
        FlatService flatService = new FlatService(flatRepository.Object);
        
        flatRepository.Setup(flatRepository => flatRepository.GetAllFlats()).Throws(new UnknownRepositoryException("Unknown error"));
        Assert.ThrowsException<UnknownServiceException>(() => flatService.GetAllFlats());
        flatRepository.VerifyAll();
    }
    
}