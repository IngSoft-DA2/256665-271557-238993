using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;
using WebModels.Responses;

namespace Test.ApiControllers
{
    [TestClass]
    public class FlatControllerTest
    {

        [TestMethod]
        public void GetAllFlats_OkIsReturned()
        {
            IEnumerable<GetFlatResponse> expectedFlats = new List<GetFlatResponse>()
            {
                new GetFlatResponse()
                {
                    Floor = 1,
                    RoomNumber = 102,
                    Owner = new OwnerResponse()
                    {
                        Name = "Barry",
                        Lastname = "White",
                        Email =         "barrywhite@gmail.com",
                    },
                    TotalRooms = 3,
                    TotalBaths = 2
                }
            };

            OkObjectResult expectedControllerResponse = new OkObjectResult(expectedFlats);

            Mock<IFlatAdapter> flatAdapter = new Mock<IFlatAdapter>(MockBehavior.Strict);
            flatAdapter.Setup(adapter => adapter.GetAllFlats()).Returns(expectedFlats);

            FlatController flatController = new FlatController(flatAdapter.Object);

            IActionResult controllerResponse = flatController.GetAllFlats();

            flatAdapter.VerifyAll();

            OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
            Assert.IsNotNull(controllerResponseCasted);

            List<GetFlatResponse>? controllerResponseValueCasted =
                controllerResponseCasted.Value as List<GetFlatResponse>;
            Assert.IsNotNull(controllerResponseValueCasted);

            Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
            Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(controllerResponseValueCasted));


        }
    }
}
