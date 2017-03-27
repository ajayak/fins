using MediatR;

namespace FINS.Features.Inventory.Items.Operations
{
    public class DeleteItemCommand : IRequest<bool>
    {
        public int ItemId { get; set; }
        public int OrganizationId { get; set; }
    }
}
