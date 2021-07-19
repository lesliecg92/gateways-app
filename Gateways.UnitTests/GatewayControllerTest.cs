using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gateways.Business;
using Gateways.Business.Models;
using Gateways.Repository.Domain;
using GatewaysApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Gateways.UnitTests
{
    public class GatewayControllerTest
    {
        private readonly GatewayController _gatewayController;
        private readonly PeripheralDeviceController _deviceController;

        public GatewayControllerTest()
        {
            IGatewayService service = new GatewayServiceFake();
            _gatewayController = new GatewayController(service);
            _deviceController = new PeripheralDeviceController(service);
        }

        [Fact]
        public async Task Get_WhenCalled_Ok()
        {
            // Act
            var okResult = await _gatewayController.Get(new Pagination { Page = 1, PageSize = 15 });

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_CheckType()
        {
            // Act
            var okResult = await _gatewayController.Get(new Pagination { Page = 1, PageSize = 15 }) as OkObjectResult;

            // Assert
            var items = Assert.IsType<ResponseModel>(okResult.Value);
            Assert.Equal(2, items.TotalCount);
        }

        [Fact]
        public async Task AddDeviceToGateway_VerifyMaxDeviceCount_BadRequest()
        {
            // Act
            var badRequest = await _deviceController.Post(1, new PeripheralDeviceModel()
            {
                DateCreated = DateTime.Now.AddDays(-15),
                Status = Status.Offline,
                UID = 12342,
                Vendor = "Vendor"
            }) as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public async Task AddDeviceToGateway_VerifyMaxDeviceCount_Ok()
        {
            // Act
            var okResult = await _deviceController.Post(2, new PeripheralDeviceModel
            {
                DateCreated = DateTime.Now.AddDays(-15),
                Status = Status.Offline,
                UID = 12342,
                Vendor = "Vendor"
            }) as OkObjectResult;

            // Assert
            var item = Assert.IsType<OkObjectResult>(okResult);
        }
    }
}
