using System.Linq;
using System.Threading.Tasks;
using FINS.Core.AutoMap;
using FINS.Core.Helpers;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Features.Accounting.Accounts.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllAccounts
            (int pageNo = 1, int pageSize = 10, string sort = "")
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

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
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();
            var account = await _mediator.Send(new GetAccountQuery()
            {
                AccountId = accountId,
                OrganizationId = organizationId
            });
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody]AccountDto account)
        {
            if (account == null || !account.ContactPersons.Any() || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addAccountCommand = account.MapTo<AddAccountCommand>();
            var addedAccount = await _mediator.Send(addAccountCommand);
            return Ok(addedAccount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody]AccountDto account)
        {
            if (account == null || !account.ContactPersons.Any() || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateAccountCommand = account.MapTo<UpdateAccountCommand>();
            var updatedAccount = await _mediator.Send(updateAccountCommand);
            return Ok(updatedAccount);
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();
            var result = await _mediator.Send(new DeleteAccountCommand()
            {
                AccountId = accountId,
                OrganizationId = organizationId
            });
            return Ok(result);
        }
    }
}
