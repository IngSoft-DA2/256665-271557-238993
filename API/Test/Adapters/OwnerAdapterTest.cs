using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ManagerResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class OwnerAdapterTest
{
    [TestMethod]
    public void GetAllOwners_ReturnsGetAllOwnersResponse()
    {
        Mock<IOwnerService> ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        OwnerAdapter ownerAdapter = new OwnerAdapter(ownerService.Object);

        IEnumerable<GetOwnerResponse> expectedAdapterResponse = new List<GetOwnerResponse>()
        {
            new GetOwnerResponse
            {
                Id = Guid.NewGuid(),
                Name = "OwnerName",
                Lastname = "OwnerLastname",
                Email = "owner@gmail.com"
            }
        };
        IEnumerable<Owner> expectedServiceResponse = new List<Owner>()
        {
            new Owner
            {
                Id = expectedAdapterResponse.First().Id,
                Firstname = expectedAdapterResponse.First().Name,
                Lastname = expectedAdapterResponse.First().Lastname,
                Email = expectedAdapterResponse.First().Email
            }
        };

        ownerService.Setup(service => service.GetAllOwners()).Returns(expectedServiceResponse);
        
        IEnumerable<GetOwnerResponse> adapterResponse = ownerAdapter.GetAllOwners();
        
        Assert.AreEqual(expectedAdapterResponse.Count(),adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod] public void GetAllOwners_ThrowsException()
    {
        Mock<IOwnerService> ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        OwnerAdapter ownerAdapter = new OwnerAdapter(ownerService.Object);

        ownerService.Setup(service => service.GetAllOwners()).Throws(new Exception());

        Assert.ThrowsException<Exception>(() => ownerAdapter.GetAllOwners());
    }






}