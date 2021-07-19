using System;
using System.Threading.Tasks;
using Gateways.Business;
using Gateways.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace GatewaysApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private static IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpPost("List")]
        public async Task<IActionResult> Get([FromBody] Pagination pagination)
        {
            if (pagination == null)
            {
                return BadRequest();
            }

            var result = await _gatewayService.GetGateways(pagination);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var gateway = await _gatewayService.GetGateway(id);

            return Ok(gateway);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Post([FromBody] GatewayModel gatewayModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _gatewayService.AddGateway(gatewayModel));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] GatewayModel gatewayModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _gatewayService.UpdateGateway(gatewayModel));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _gatewayService.RemoveGateway(id));
        }
    }
}
