using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AddAccountGroupCommand : AccountGroupDto, IRequest<AccountGroupDto>
    {
        public int OrganizationId { get; set; }
    }
}
