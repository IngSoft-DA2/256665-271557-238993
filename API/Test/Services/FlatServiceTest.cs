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

    [TestInitialize]
    public void Initialize()
    {
        _flatService = new FlatService();
    }

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
            RoomNumber = "101",
            OwnerAssigned = new Owner(),
            TotalRooms = 2,
            TotalBaths = 1,
            HasTerrace = true
        };
        _flatService.CreateFlat(flatToAdd);
    }

    #region Create Flat, Domain Validations

    [TestMethod]
    public void CreateFlatWithNoTotalRooms_ThrowsObjectErrorServiceException()
    {
        Flat flatWithNoRooms = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = "101",
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
            RoomNumber = "101",
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
    public void GivenUnAssignedOwnerOnCreate_ShouldThrowUnknownServiceException()
    {
        Flat flatToAdd = new Flat
        {
            Id = Guid.NewGuid(),
            Floor = 1,
            RoomNumber = "101",
            OwnerAssigned = null,
            TotalRooms = 1,
            TotalBaths = 1,
            HasTerrace = true
        };

        Assert.ThrowsException<UnknownServiceException>(() => _flatService.CreateFlat(flatToAdd));
    }

    #endregion

    #endregion
}