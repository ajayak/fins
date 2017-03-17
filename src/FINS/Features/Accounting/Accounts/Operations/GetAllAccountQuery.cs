using FINS.Core.Helpers;
using FINS.Features.Accounting.Accounts.DTO;
using MediatR;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class GetAllAccountQuery : BasePaging, IRequest<PagedResult<AccountListDto>>
    {
        public int OrganizationId { get; set; }
    }
}
