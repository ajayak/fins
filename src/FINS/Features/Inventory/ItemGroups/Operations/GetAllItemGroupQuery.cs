using System.Collections.Generic;
using MediatR;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class GetAllItemGroupQuery : IRequest<List<ItemGroupDto>>
    {
        public int OrganizationId { get; set; }
    }
}
