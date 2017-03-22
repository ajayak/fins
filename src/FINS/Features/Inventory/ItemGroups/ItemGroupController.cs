using System.Threading.Tasks;
using FINS.Core.AutoMap;
using FINS.Core.FinsAttributes;
using FINS.Core.Helpers;
using FINS.Features.Inventory.ItemGroups.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Inventory.ItemGroups
{
    [Authorize]
    [Route("api/[controller]")]
    public class ItemGroupController : Controller
    {
        private readonly IMediator _mediator;

        public ItemGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllItemGroups()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var itemGroups = await _mediator.Send(new GetAllItemGroupQuery() { OrganizationId = organizationId });

            return Ok(itemGroups);
        }

        [HttpGet("list"), Produces("application/json")]
        public async Task<IActionResult> GetAllPrimaryItemGroupsCollection()
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var itemGroups = await _mediator.Send(new GetPrimaryItemGroupDictionaryQuery() { OrganizationId = organizationId });

            return Ok(itemGroups);
        }

        [ItemGroupCreator]
        [HttpPost]
        public async Task<IActionResult> AddItemGroup([FromBody]ItemGroupDto itemGroup)
        {
            if (itemGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var query = itemGroup.MapTo<AddItemGroupCommand>();
            query.OrganizationId = organizationId;
            var addedItemGroup = await _mediator.Send(query);
            return Ok(addedItemGroup);
        }

        [ItemGroupCreator]
        [HttpPut]
        public async Task<IActionResult> UpdateItemGroup([FromBody]ItemGroupDto itemGroup)
        {
            if (itemGroup == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var query = itemGroup.MapTo<UpdateItemGroupCommand>();
            query.OrganizationId = organizationId;
            var updatedItemGroup = await _mediator.Send(query);
            return Ok(updatedItemGroup);
        }

        [ItemGroupCreator]
        [HttpDelete("{itemGroupId}")]
        public async Task<IActionResult> DeleteItemGroup(int itemGroupId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();

            var query = new DeleteItemGroupCommand
            {
                OrganizationId = organizationId,
                ItemGroupId = itemGroupId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
