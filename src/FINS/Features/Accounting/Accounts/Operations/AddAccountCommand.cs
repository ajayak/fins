using FINS.Features.Accounting.Accounts.DTO;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class AddAccountCommand : AccountDto, IRequest<AccountDto>
    {
    }
}
