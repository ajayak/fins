using System.Collections.Generic;
using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class GetAllAccountGroupQuery : IRequest<List<AccountGroupDto>>
    {
        public int OrganizationId { get; set; }
    }
}
