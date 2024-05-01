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

        [TestMethod]
        public void CreateFlatRequest_BadRequestIsReturned()
        {
            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Throws(new ObjectErrorAdapterException("Owner can't be null"));

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());

            BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Owner can't be null");

            _flatAdapter.VerifyAll();

            BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        }

        [TestMethod]
        public void CreateFlatRequest_500StatusCodeIsReturned()
        {
            ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
            expectedControllerResponse.StatusCode = 500;

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Throws(new Exception("An specific error on the server"));

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());
            _flatAdapter.VerifyAll();

            ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(controllerResponseCasted.Value, expectedControllerResponse.Value);
            Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
        }

        [TestMethod]
        public void CreateFlatRequest_NotFoundIsReturned()
        {
            NotFoundObjectResult expectedControllerResponse =
                new NotFoundObjectResult("Owner was not found in database");

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Throws(new ObjectNotFoundAdapterException());

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());
            _flatAdapter.VerifyAll();

            NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        }

        #endregion

    }
}