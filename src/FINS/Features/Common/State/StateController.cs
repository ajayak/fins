using System.Threading.Tasks;
using FINS.Core.Helpers;
using FINS.Features.Common.State.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Common.State
{
    [Authorize]
    [Route("api/[controller]")]
    public class StateController : Controller
    {
        private readonly IMediator _mediator;

        public StateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllStates()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var states = await _mediator.Send(new GetAllStatesQuery { OrganizationId = organizationId });
            return Ok(states);
        }
    }
}
