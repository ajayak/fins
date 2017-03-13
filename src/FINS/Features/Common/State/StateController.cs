using System.Threading.Tasks;
using FINS.Features.Common.State.Operations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Common.State
{
    [Route("api/[controller]")]
    public class StateController : Controller
    {
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(""), Produces("application/json")]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _mediator.Send(new GetAllStatesQuery());
            return Ok(states);
        }
    }
}
