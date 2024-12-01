using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Overlord.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ISender _sender;
        public PingController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string Get()
        {
            return "pong";
        }

        [HttpGet("really")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string ReallyGet()
        {
            return "really pong";
        }

    }
}
