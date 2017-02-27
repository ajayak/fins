using System;
using System.Linq;
using System.Threading.Tasks;
using FINS.AutoMap;
using FINS.Context;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AddAccountGroupCommandHandler : IAsyncRequestHandler<AddAccountGroupCommand, AccountGroupDto>
    {
        private readonly FinsDbContext _context;

        public AddAccountGroupCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new Account group
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AccountGroupDto> Handle(AddAccountGroupCommand message)
        {
            if (!CheckParentOrganizationIdExists(message.ParentId))
            {
                throw new Exception("Parent organization does not exist");
            }
            if (message.ParentId != 0)
            {
                var parentAccountGroup = await _context.AccountGroups.FindAsync(message.ParentId);
                parentAccountGroup.IsPrimary = false;
            }
            if (await AccountGroupExistsInOrganization(message))
            {
                throw new Exception("Account Group with same name already exists under this parent");
            }

            var accountGroup = message.MapTo<AccountGroup>();
            accountGroup.IsPrimary = true;
            await _context.AccountGroups.AddAsync(accountGroup);

            await _context.SaveChangesAsync();
            return accountGroup.MapTo<AccountGroupDto>();
        }

        private bool CheckParentOrganizationIdExists(int parentId)
        {
            return parentId == 0 || _context.AccountGroups.Any(c => c.Id == parentId);
        }

        private async Task<bool> AccountGroupExistsInOrganization(AddAccountGroupCommand message)
        {
            return await _context.AccountGroups
                .AnyAsync(c => c.ParentId == message.ParentId &&
                               c.OrganizationId == message.OrganizationId &&
                               !c.IsDeleted &&
                               c.Name.Equals(message.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
