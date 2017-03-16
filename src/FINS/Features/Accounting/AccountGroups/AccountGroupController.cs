using System;
using System.Threading.Tasks;
using FINS.Core.AutoMap;
using FINS.Core.FinsAttributes;
using FINS.Core.Helpers;
using FINS.Features.Accounting.AccountGroups.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllAccountGroups()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var accountGroups = await _mediator.Send(new GetAllAccountGroupQuery() { OrganizationId = organizationId });

            return Ok(accountGroups);
        }

        [HttpGet("list"), Produces("application/json")]
        public async Task<IActionResult> GetAllPrimaryAccountGroupsCollection()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var accountGroups = await _mediator.Send(new GetPrimaryAccountGroupDictionaryQuery() { OrganizationId = organizationId });

            return Ok(accountGroups);
        }

        [AccountGroupCreator]
        [HttpPost]
        public async Task<IActionResult> AddAccountGroup([FromBody]AccountGroupDto accountGroup)
        {
            if (accountGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var query = accountGroup.MapTo<AddAccountGroupCommand>();
            query.OrganizationId = organizationId;
            var addedAccountGroup = await _mediator.Send(query);
            return Ok(addedAccountGroup);
        }

        [AccountGroupCreator]
        [HttpPut]
        public async Task<IActionResult> UpdateAccountGroup([FromBody]AccountGroupDto accountGroup)
        {
            if (accountGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var query = accountGroup.MapTo<UpdateAccountGroupCommand>();
            query.OrganizationId = organizationId;
            var updatedAccountGroup = await _mediator.Send(query);
            return Ok(updatedAccountGroup);
        }

        [AccountGroupCreator]
        [HttpDelete("{accountGroupId}")]
        public async Task<IActionResult> DeleteAccountGroup(int accountGroupId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

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
