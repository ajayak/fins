using System.Collections.Generic;
using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class GetAccountGroupDictionaryQuery : IRequest<Dictionary<int, string>>
    {
        public int OrganizationId { get; set; }
    }
}
