using FINS.Features.Accounting.Accounts.DTO;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class UpdateAccountCommand : AccountDto, IRequest<AccountDto>
    {
    }
}
