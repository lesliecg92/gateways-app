using System.Threading.Tasks;
using Gateways.Business;
using Gateways.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace GatewaysApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralDeviceController : ControllerBase
    {
        private static IGatewayService _gatewayService;

        public PeripheralDeviceController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetDevicesByGateway(int gatewayId)
        {
            var result = await _gatewayService.GetDevices(gatewayId);

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int deviceId)
        {
            var gateway = await _gatewayService.GetDevice(deviceId);

            return Ok(gateway);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Post(int gatewayId, [FromBody] PeripheralDeviceModel peripheralDeviceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //ValidationProblemDetails
            if (!await _gatewayService.GatewayAcceptsDevice(gatewayId))
            {
                ModelState.AddModelError("Device", "Only 10 devices are allowed per gateway");
                //ModelState.
                return ValidationProblem();
            }

            return Ok(_gatewayService.AddDevice(gatewayId, peripheralDeviceModel));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] PeripheralDeviceModel peripheralDeviceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _gatewayService.UpdateDevice(peripheralDeviceModel));
        }

        [HttpDelete("Delete/{id}")]
        public async Task Delete(int id)
        {
            await _gatewayService.RemoveDevice(id);
        }
    }
}
