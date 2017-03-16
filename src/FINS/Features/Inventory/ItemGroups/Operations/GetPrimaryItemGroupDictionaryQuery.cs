using System.Collections.Generic;
using MediatR;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class GetPrimaryItemGroupDictionaryQuery : IRequest<Dictionary<int, string>>
    {
        public int OrganizationId { get; set; }
    }
}
