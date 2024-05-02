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

        #endregion
    }
}