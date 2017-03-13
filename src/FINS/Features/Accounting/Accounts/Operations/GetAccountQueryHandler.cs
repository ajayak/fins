using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Features.Accounting.Accounts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class GetAccountQueryHandler : IAsyncRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly FinsDbContext _context;

        public GetAccountQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> Handle(GetAccountQuery query)
        {
            var account = await _context.Accounts
                .Include(c => c.ContactPersons)
                .FirstOrDefaultAsync(c =>
                    c.Id == query.AccountId &&
                    c.AccountGroup.OrganizationId == query.OrganizationId);
            return account.MapTo<AccountDto>();
        }
    }
}
