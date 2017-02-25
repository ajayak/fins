using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AddAccountGroupQuery : AccountGroupDto, IRequest<AccountGroupDto>
    {
        public int OrganizationId { get; set; }
    }
}
