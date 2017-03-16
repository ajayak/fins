using MediatR;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class UpdateItemGroupCommand : AddItemGroupCommand, IRequest<ItemGroupDto>
    {
        
    }
}
