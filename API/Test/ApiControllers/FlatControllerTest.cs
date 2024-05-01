using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers
{
    [TestClass]
    public class FlatControllerTest
    {
        #region Initilizing aspects

        private Mock<IFlatAdapter> _flatAdapter;
        private FlatController _flatController;

        [TestInitialize]
        public void Initialize()
        {
            _flatAdapter = new Mock<IFlatAdapter>(MockBehavior.Strict);
            _flatController = new FlatController(_flatAdapter.Object);
        }

        #endregion

        #region GetAllFlats

        [TestMethod]
        public void GetAllFlats_OkIsReturned()
        {
            IEnumerable<GetFlatResponse> expectedFlats = new List<GetFlatResponse>()
            {
                new GetFlatResponse()
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = 102,
                    OwnerAssigned = new GetOwnerResponse()
                    {
                        Firstname = "Barry",
                        Lastname = "White",
                        Email = "barrywhite@gmail.com",
                    },
                    TotalRooms = 3,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            };

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedFlats);

            _flatAdapter.Setup(adapter => adapter.GetAllFlats(It.IsAny<Guid>())).Returns(expectedFlats);

            IActionResult controllerResponse = _flatController.GetAllFlats(It.IsAny<Guid>());

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            List<GetFlatResponse>? controllerResponseValueCasted =
                controllerResponseCasted.Value as List<GetFlatResponse>;
            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(controllerResponseValueCasted));
        }

       
        #endregion

        #region CreateFlat

        [TestMethod]
        public void CreateFlatRequest_AcceptedIsReturned()
        {
            AcceptedResult expectedControllerResponse = new AcceptedResult();

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()));

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());

            _flatAdapter.VerifyAll();

            AcceptedResult? controllerResponseCasted = controllerResponse as AcceptedResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        }

       
        

        

        #endregion

        #region GetFlatById

        [TestMethod]
        public void GetFlatById_OkIsReturned()
        {
            GetFlatResponse expectedFlat = new GetFlatResponse()
            {
                Id = Guid.NewGuid(),
                Floor = 1,
                RoomNumber = 102,
                OwnerAssigned = new GetOwnerResponse()
                {
                    Firstname = "Barry",
                    Lastname = "White",
                    Email = "barrywhite@gmail.com"
                },
                HasTerrace = true
            };
            _flatAdapter.Setup(adapter => adapter.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(expectedFlat);

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedFlat);

            IActionResult controllerResponse = _flatController.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>());

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;

            Assert.IsNotNull(controllerResponseCasted);

            GetFlatResponse? controllerResponseValueCasted = controllerResponseCasted.Value as GetFlatResponse;

            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedFlat, controllerResponseValueCasted);
            Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
        }

        #endregion
    }
}