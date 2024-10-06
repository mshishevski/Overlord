using Microsoft.AspNetCore.Mvc;
using Overlord.Application.Interfaces;

namespace Overlord.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionsController : ControllerBase
    {
        private readonly IMqttBrokerService _mqttBrokerService;

        public ActionsController(IMqttBrokerService mqttBrokerService)
        {
            _mqttBrokerService = mqttBrokerService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartBroker()
        {
            await _mqttBrokerService.StartAsync(CancellationToken.None);
            return Ok("Broker started");
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopBroker()
        {
            await _mqttBrokerService.StopAsync(CancellationToken.None);
            return Ok("Broker stopped");
        }
    }
}
