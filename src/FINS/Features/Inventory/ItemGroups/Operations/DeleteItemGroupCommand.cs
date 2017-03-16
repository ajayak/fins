using MediatR;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class DeleteItemGroupCommand : IRequest<bool>
    {
        public int ItemGroupId { get; set; }
        public int OrganizationId { get; set; }
    }
}
