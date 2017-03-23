using System.Threading.Tasks;
using FINS.Core.Helpers;
using FINS.Features.Common.Unit.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Common.Unit
{
    [Authorize]
    [Route("api/[controller]")]
    public class UnitController : Controller
    {
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllUnits()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var states = await _mediator.Send(new GetAllUnitsQuery { OrganizationId = organizationId });
            return Ok(states);
        }
    }
}
