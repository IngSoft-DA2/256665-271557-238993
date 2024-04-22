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
    #region Initialize

    private Mock<IOwnerService> _ownerService;
    private OwnerAdapter _ownerAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        _ownerAdapter = new OwnerAdapter(_ownerService.Object);
    }

    #endregion

    #region Get All Owners

    [TestMethod]
    public void GetAllOwners_ReturnsGetAllOwnersResponse()
    {
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

        _ownerService.Setup(service => service.GetAllOwners()).Returns(expectedServiceResponse);

        IEnumerable<GetOwnerResponse> adapterResponse = _ownerAdapter.GetAllOwners();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllOwners_ThrowsException()
    {
        _ownerService.Setup(service => service.GetAllOwners()).Throws(new Exception());
        Assert.ThrowsException<Exception>(() => _ownerAdapter.GetAllOwners());
    }

    #endregion
    
    
}