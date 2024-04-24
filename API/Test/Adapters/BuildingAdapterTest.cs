using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class BuildingAdapterTest
{
    [TestMethod]
    public void GetAllBuilding_ReturnsGetBuildingResponses()
    {
        IEnumerable<Building> expectedServiceResponse = new List<Building>
        {
            new Building
            {
                Id = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = new Location
                {
                    Latitude = 1.23,
                    Longitude = 4.56
                },
                ConstructionCompany = new ConstructionCompany
                {
                    Id = Guid.NewGuid(),
                    Name = "constructionCompany"
                },
                CommonExpenses = 1000,
                Flats = new List<Flat>
                {
                    new Flat
                    {
                        Id = Guid.NewGuid(),
                        Floor = 1,
                        RoomNumber = 102,
                        OwnerAssigned = new Owner
                        {
                            Id = Guid.NewGuid(),
                            Firstname = "OwnerFirstname",
                            Lastname = "OwnerLastname",
                            Email = "owner@gmail.com",
                            Flats = new List<Flat>()
                        },
                        TotalRooms = 4,
                        TotalBaths = 2,
                        HasTerrace = true
                    }
                }
            }
        };
        IEnumerable<GetBuildingResponse> expectedAdapterResponse = new List<GetBuildingResponse>
        {
            new GetBuildingResponse
            {
                Id = expectedServiceResponse.First().Id,
                Name = expectedServiceResponse.First().Name,
                Address = expectedServiceResponse.First().Address,
                Location = new LocationResponse
                {
                    Latitude = expectedServiceResponse.First().Location.Latitude,
                    Longitude = expectedServiceResponse.First().Location.Longitude
                },
                ConstructionCompany = new GetConstructionCompanyResponse
                {
                    Id = expectedServiceResponse.First().ConstructionCompany.Id,
                    Name = expectedServiceResponse.First().ConstructionCompany.Name,
                },
                CommonExpenses = 1000,
                Flats = new List<GetFlatResponse>
                {
                    new GetFlatResponse
                    {
                        Id = expectedServiceResponse.First().Flats.First().Id,
                        Floor = expectedServiceResponse.First().Flats.First().Floor,
                        RoomNumber = expectedServiceResponse.First().Flats.First().RoomNumber,
                        OwnerAssigned = new GetOwnerResponse()
                        {
                            Id = expectedServiceResponse.First().Flats.First().OwnerAssigned.Id,
                            Firstname = expectedServiceResponse.First().Flats.First().OwnerAssigned.Firstname,
                            Lastname = expectedServiceResponse.First().Flats.First().OwnerAssigned.Lastname,
                            Email = expectedServiceResponse.First().Flats.First().OwnerAssigned.Email
                        },
                        TotalRooms = expectedServiceResponse.First().Flats.First().TotalRooms,
                        TotalBaths = expectedServiceResponse.First().Flats.First().TotalBaths,
                        HasTerrace = expectedServiceResponse.First().Flats.First().HasTerrace
                    }
                }
            }
        };
        
        Mock<IBuildingService> buildingService = new Mock<IBuildingService>(MockBehavior.Strict);
        buildingService.Setup(service => service.GetAllBuildings()).Returns(expectedServiceResponse);

        BuildingAdapter buildingAdapter = new BuildingAdapter(buildingService.Object);

        IEnumerable<GetBuildingResponse> adapterResponse = buildingAdapter.GetAllBuildings();
        buildingService.VerifyAll();
        
        Assert.AreEqual(expectedAdapterResponse.Count(),adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllBuildings_ThrowsUnknownAdapterException()
    {
        Mock<IBuildingService> buildingService = new Mock<IBuildingService>(MockBehavior.Strict);
        buildingService.Setup(service => service.GetAllBuildings()).Throws<Exception>();

        BuildingAdapter buildingAdapter = new BuildingAdapter(buildingService.Object);

        Assert.ThrowsException<UnknownAdapterException>(() => buildingAdapter.GetAllBuildings());
        
    }
}