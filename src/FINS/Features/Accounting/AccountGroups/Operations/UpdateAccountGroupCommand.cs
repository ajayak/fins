using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class UpdateAccountGroupCommand : AddAccountGroupCommand, IRequest<AccountGroupDto>
    {
        
    }
}
