using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class UpdateAccountGroupQuery : AddAccountGroupQuery, IRequest<AccountGroupDto>
    {
        
    }
}
