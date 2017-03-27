using FINS.Features.Inventory.Items.DTO;
using MediatR;

namespace FINS.Features.Inventory.Items.Operations
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public int ItemId { get; set; }
        public int OrganizationId { get; set; }
    }
}
