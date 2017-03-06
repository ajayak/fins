using System.Threading.Tasks;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Features.Accounting.Accounts.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FINS.Features.Accounting.Accounts
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [HttpGet("{organizationId}"), Produces("application/json")]
        public async Task<IActionResult> GetAllAccounts
            (int organizationId = 0, int pageNo = 1, int pageSize = 10, string sort = "")
        {
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var accountList = await _mediator.Send(new GetAllAccountQuery()
            {
                OrganizationId = organizationId,
                Sort = sort,
                PageNo = pageNo,
                PageSize = pageSize
            });

            return Ok(accountList);
        }

        [HttpGet("{accountId}"), Produces("application/json")]
        public async Task<IActionResult> GetAccount(int accountId, int organizationId = 0)
        {
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;


            return Ok();
        }
    }
}
