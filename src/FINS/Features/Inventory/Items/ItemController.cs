using System.Threading.Tasks;
using FINS.Core.Helpers;
using FINS.Features.Accounting.Accounts.Operations;
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

            var itemList = await _mediator.Send(new GetAllItemQuery
            {
                OrganizationId = organizationId,
                Sort = sort,
                PageNo = pageNo,
                PageSize = pageSize
            });

            return Ok(itemList);
        }
    }
}
