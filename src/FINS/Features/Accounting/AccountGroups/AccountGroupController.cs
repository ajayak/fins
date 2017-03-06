using System;
using System.Threading.Tasks;
using FINS.Core.AutoMap;
using FINS.Core.FinsAttributes;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Accounting.AccountGroups
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountGroupController : Controller
    {
        private readonly IMediator _mediator;

        public AccountGroupController(IMediator mediator)
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

        [HttpGet("list")]
        [HttpGet("list/{organizationId}"), Produces("application/json")]
        public async Task<IActionResult> GetAccountGroupsCollection(int organizationId = 0)
        {
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var accountGroups = await _mediator.Send(new GetAccountGroupDictionaryQuery() { OrganizationId = organizationId });

            return Ok(accountGroups);
        }

        [AccountGroupCreator]
        [HttpPost("")]
        [HttpPost("{organizationId}")]
        public async Task<IActionResult> AddAccountGroup([FromBody]AccountGroupDto accountGroup, int organizationId = 0)
        {
            if (accountGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var query = accountGroup.MapTo<AddAccountGroupCommand>();
            query.OrganizationId = organizationId;
            var addedAccountGroup = await _mediator.Send(query);
            return Ok(addedAccountGroup);
        }

        [AccountGroupCreator]
        [HttpPut("")]
        [HttpPut("{organizationId}")]
        public async Task<IActionResult> UpdateAccountGroup([FromBody]AccountGroupDto accountGroup, int organizationId = 0)
        {
            if (accountGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var query = accountGroup.MapTo<UpdateAccountGroupCommand>();
            query.OrganizationId = organizationId;
            var updatedAccountGroup = await _mediator.Send(query);
            return Ok(updatedAccountGroup);
        }

        [AccountGroupCreator]
        [HttpDelete("{accountGroupId}")]
        [HttpDelete("{accountGroupId}/organization/{organizationId}")]
        public async Task<IActionResult> DeleteAccountGroup(int accountGroupId, int organizationId = 0)
        {
            var orgId = User.GetOrganizationId();
            organizationId = orgId ?? organizationId;

            var query = new DeleteAccountGroupCommand
            {
                OrganizationId = organizationId,
                AccountGroupId = accountGroupId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
