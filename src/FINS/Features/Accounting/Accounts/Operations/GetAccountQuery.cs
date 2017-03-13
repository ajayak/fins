using FINS.Features.Accounting.Accounts.DTO;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class GetAccountQuery : IRequest<AccountDto>
    {
        public int AccountId { get; set; }
        public int OrganizationId { get; set; }
    }
}
