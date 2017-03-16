using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.FinsExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.Accounts.Operations
{
    public class DeleteAccountCommandHandler : IAsyncRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly FinsDbContext _context;

        public DeleteAccountCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAccountCommand query)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(c => c.Id == query.AccountId &&
                            c.AccountGroup.OrganizationId == query.OrganizationId);
            if (account == null)
            {
                throw new FinsNotFoundException("No matching account found!");
            }
            account.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
