using System.Threading.Tasks;
using FINS.Core.AutoMap;
using FINS.Core.FinsAttributes;
using FINS.Core.Helpers;
using FINS.Features.Inventory.Items.DTO;
using FINS.Features.Inventory.Items.Operations;
using FINS.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FINS.Features.Inventory.Items
{
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IMediator _mediator;
        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAllItems
            (int pageNo = 1, int pageSize = 10, string sort = "")
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var itemList = await _mediator.Send(new GetAllItemQuery
            {
                OrganizationId = organizationId,
                Sort = sort,
                PageNo = pageNo,
                PageSize = pageSize,
                BaseUrl = baseUrl
            });

            return Ok(itemList);
        }

        [HttpGet("{itemId}"), Produces("application/json")]
        public async Task<IActionResult> GetItem(int itemId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();
            var item = await _mediator.Send(new GetItemQuery()
            {
                ItemId = itemId,
                OrganizationId = organizationId
            });
            return Ok(item);
        }

        [HttpPost]
        [ItemCreator]
        public async Task<IActionResult> AddItem([FromBody]ItemDto item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addItemCommand = item.MapTo<AddItemCommand>();
            var addedItem = await _mediator.Send(addItemCommand);
            return Ok(addedItem);
        }

        [HttpPut]
        [ItemCreator]
        public async Task<IActionResult> UpdateItem([FromBody]ItemDto item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateItemCommand = item.MapTo<UpdateItemCommand>();
            var updatedItem = await _mediator.Send(updateItemCommand);
            return Ok(updatedItem);
        }

        [ItemCreator]
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteAccount(int itemId)
        {
            var orgId = User.GetOrganizationId();
            var organizationId = orgId ?? HttpContext.Request.Headers.GetOrgIdFromHeader();
            var result = await _mediator.Send(new DeleteItemCommand()
            {
                ItemId = itemId,
                OrganizationId = organizationId
            });
            return Ok(result);
        }
    }
}
