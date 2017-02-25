﻿using System;
using System.Threading.Tasks;
using FINS.AutoMap;
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

            var query = accountGroup.MapTo<AddAccountGroupQuery>();
            query.OrganizationId = organizationId;
            try
            {
                var accountGroups = await _mediator.Send(query);
                return Ok(accountGroups);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
