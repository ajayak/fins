using System;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class DeleteAccountGroupQueryHandler : IAsyncRequestHandler<DeleteAccountGroupQuery, bool>
    {
        private readonly FinsDbContext _context;

        public DeleteAccountGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAccountGroupQuery message)
        {
            if (await AccountGroupIsParent(message.AccountGroupId))
            {
                throw new Exception("Account group has related child account groups.");
            }
            if (await AccountGroupHasAccounts(message.AccountGroupId))
            {
                throw new Exception("Account group has related active accounts.");
            }
            var accountGroup = _context.AccountGroups.Find(message.AccountGroupId);
            if (accountGroup == null)
            {
                throw new Exception("No matching Account group found.");
            }
            accountGroup.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> AccountGroupIsParent(int accountGroupId)
        {
            return await _context.AccountGroups
                .AnyAsync(c => c.ParentId == accountGroupId && !c.IsDeleted);
        }

        private async Task<bool> AccountGroupHasAccounts(int accountGroupId)
        {
            return await _context.Accounts
                .AnyAsync(c => c.AccountGroupId == accountGroupId && !c.IsDeleted);
        }
    }
}
