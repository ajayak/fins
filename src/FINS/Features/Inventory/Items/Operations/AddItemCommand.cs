using FINS.Features.Inventory.Items.DTO;
using MediatR;

namespace FINS.Features.Inventory.Items.Operations
{
    public class AddItemCommand : ItemDto, IRequest<ItemDto>
    {
    }
}
