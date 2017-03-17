using FINS.Core.Helpers;
using FINS.Features.Inventory.Items.DTO;
using MediatR;

namespace FINS.Features.Inventory.Items.Operations
{
    public class GetAllItemQuery : BasePaging, IRequest<PagedResult<ItemListDto>>
    {
        public int OrganizationId { get; set; }
    }
}
