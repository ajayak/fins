using MediatR;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class AddItemGroupCommand : ItemGroupDto, IRequest<ItemGroupDto>
    {
        public int OrganizationId { get; set; }
    }
}
