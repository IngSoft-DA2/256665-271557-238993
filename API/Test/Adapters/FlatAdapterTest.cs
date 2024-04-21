using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class FlatAdapterTest
{
    [TestMethod]
    public void GetAllFlats_ShouldConvertFlatsReceived_IntoGetFlatResponses()
    {
        IEnumerable<Flat> expectedServiceResponse = new List<Flat>
        {
            new Flat
            {
                Floor = 1,
                RoomNumber = 102,
                OwnerAssigned = new Owner
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Michael",
                    Lastname = "Kent",
                    Email = "owner@gmail.com",
                },
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            }
        };

        IEnumerable<GetFlatResponse> expectedAdapterResponse = expectedServiceResponse.Select(flatResponse =>
            new GetFlatResponse
            {
                Id = flatResponse.Id,
                Floor = flatResponse.Floor,
                RoomNumber = flatResponse.RoomNumber,
                GetOwnerAssigned = new GetOwnerAssignedResponse()
                {
                    Id = flatResponse.OwnerAssigned.Id,
                    Firstname = flatResponse.OwnerAssigned.Firstname,
                    Lastname = flatResponse.OwnerAssigned.Lastname,
                    Email = flatResponse.OwnerAssigned.Email
                },
                TotalRooms = flatResponse.TotalRooms,
                TotalBaths = flatResponse.TotalBaths,
                HasTerrace = flatResponse.HasTerrace
            });


        Mock<IFlatService> flatService = new Mock<IFlatService>(MockBehavior.Strict);
        flatService.Setup(service => service.GetAllFlats(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        FlatAdapter flatAdapter = new FlatAdapter(flatService.Object);

        IEnumerable<GetFlatResponse> adapterResponse = flatAdapter.GetAllFlats(It.IsAny<Guid>());
        flatService.VerifyAll();
        
        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllFlats_ShouldThrowNotFoundException()
    {
        Mock<IFlatService> flatService = new Mock<IFlatService>(MockBehavior.Strict);
        flatService.Setup(service => service.GetAllFlats(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        FlatAdapter flatAdapter = new FlatAdapter(flatService.Object);
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => flatAdapter.GetAllFlats(It.IsAny<Guid>()));
        flatService.VerifyAll();
    }
    
}