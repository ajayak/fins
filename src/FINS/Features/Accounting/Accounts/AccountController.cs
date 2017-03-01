using System.Threading.Tasks;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Accounting.Accounts
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [HttpGet("{organizationId}"), Produces("application/json")]
        public async Task<IActionResult> GetAllAccountGroups(int organizationId = 0)
        {
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var accountGroups = await _mediator.Send(new GetAllAccountGroupQuery() { OrganizationId = organizationId });

            return Ok(accountGroups);
        }
    }
}
