using FINS.Features.Inventory.Items.DTO;
using MediatR;

namespace FINS.Features.Inventory.Items.Operations
{
    public class UpdateItemCommand : ItemDto, IRequest<ItemDto>
    {
    }
}
