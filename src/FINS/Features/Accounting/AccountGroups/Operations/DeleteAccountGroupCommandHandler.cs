using System;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class DeleteAccountGroupCommandHandler : IAsyncRequestHandler<DeleteAccountGroupCommand, bool>
    {
        private readonly FinsDbContext _context;

        public DeleteAccountGroupCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAccountGroupCommand message)
        {
            if (await AccountGroupIsParent(message.AccountGroupId))
            {
                throw new Exception("Account group has related child account groups.");
            }
            if (await AccountGroupHasAccounts(message.AccountGroupId))
            {
                throw new Exception("Account group has related active accounts.");
            }
            var accountGroup = await _context.AccountGroups.FindAsync(message.AccountGroupId);
            if (accountGroup == null)
            {
                throw new Exception("No matching Account group found.");
            }
            accountGroup.IsDeleted = true;
            accountGroup.IsPrimary = false;
            await CheckAndMakeAccountGroupParentPrimary(accountGroup.ParentId, accountGroup.Id);
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

        private async Task CheckAndMakeAccountGroupParentPrimary(int accountGroupParentId, int accountGroupId)
        {
            var accountGroupSiblings = await _context.AccountGroups
                .AnyAsync(c => c.ParentId == accountGroupParentId &&
                               c.Id != accountGroupId && !c.IsDeleted);
            if(accountGroupSiblings) return;
            var accountGroupParent = await _context.AccountGroups.FindAsync(accountGroupParentId);
            accountGroupParent.IsPrimary = true;
        }
    }
}
