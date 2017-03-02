using FINS.Core.Helpers;
using FINS.Models.Accounting;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class GetAllAccountQuery : BasePaging, IRequest<PagedResult<AccountDto>>
    {
        public int OrganizationId { get; set; }
    }
}
