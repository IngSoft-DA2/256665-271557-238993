using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Requests;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;
using WebModel.Responses;
using WebModel.Responses.MaintenanceRequestResponses;

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
        public void CreateFlatRequest_OkIsReturned()
        {
            CreateFlatResponse expectedAdapterResponse = new CreateFlatResponse()
            {
                Id = Guid.NewGuid()
            };

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedAdapterResponse);

            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Returns(expectedAdapterResponse);

            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            CreateFlatResponse? controllerResponseValueCasted =
                controllerResponseCasted.Value as CreateFlatResponse;
            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.AreEqual(controllerResponseValueCasted.Id, controllerResponseValueCasted.Id);
        }

        [TestMethod]
        public void CreateFlatRequest_BadRequestIsReturned()
        {
            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Throws(new ObjectErrorException("Owner can't be null"));

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
            NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("Owner was not found in database");
            
            _flatAdapter.Setup(adapter => adapter.CreateFlat(It.IsAny<CreateFlatRequest>()))
                .Throws(new ObjectNotFoundException());
            
            IActionResult controllerResponse = _flatController.CreateFlat(It.IsAny<CreateFlatRequest>());
            _flatAdapter.VerifyAll();
            
            NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
            Assert.IsNotNull(controllerResponseCasted);
            
            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
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
                GetOwnerAssigned = new GetOwnerAssignedResponse()
                {
                    Name = "Barry",
                    Lastname = "White",
                    Email = "barrywhite@gmail.com"
                },
                HasTerrace = true
            };
            _flatAdapter.Setup(adapter => adapter.GetFlatById(It.IsAny<Guid>())).Returns(expectedFlat);

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedFlat);

            IActionResult controllerResponse = _flatController.GetFlatById(It.IsAny<Guid>());

            _flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;

            Assert.IsNotNull(controllerResponseCasted);

            GetFlatResponse? controllerResponseValueCasted = controllerResponseCasted.Value as GetFlatResponse;

            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedFlat, controllerResponseValueCasted);
            Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
        }

        [TestMethod]
        public void GetFlatById_NotFoundIsReturned()
        {
            NotFoundObjectResult expectedResponse = new NotFoundObjectResult("Flat was not found, reload the page");

            _flatAdapter.Setup(adapter => adapter.GetFlatById(It.IsAny<Guid>()))
                .Throws(new ObjectNotFoundException());

            IActionResult controllerResponse = _flatController.GetFlatById(It.IsAny<Guid>());
            _flatAdapter.VerifyAll();

            NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;

            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(controllerResponseCasted.Value, expectedResponse.Value);
            Assert.AreEqual(expectedResponse.StatusCode, controllerResponseCasted.StatusCode);
        }
        
        [TestMethod]
        public void GetFlatById_500StatusCodeIsReturned()
        {
            ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
            expectedControllerResponse.StatusCode = 500;

            _flatAdapter.Setup(adapter => adapter.GetFlatById(It.IsAny<Guid>()))
                .Throws(new Exception("An specific error on the server"));

            IActionResult controllerResponse = _flatController.GetFlatById(It.IsAny<Guid>());
            _flatAdapter.VerifyAll();

            ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            Assert.AreEqual(controllerResponseCasted.Value, expectedControllerResponse.Value);
            Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
        }
        #endregion
    }
}