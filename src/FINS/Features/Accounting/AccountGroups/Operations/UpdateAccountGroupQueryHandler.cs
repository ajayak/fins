using System;
using System.Threading.Tasks;
using FINS.AutoMap;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class UpdateAccountGroupQueryHandler
        : IAsyncRequestHandler<UpdateAccountGroupQuery, AccountGroupDto>
    {
        private readonly FinsDbContext _context;

        public UpdateAccountGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<AccountGroupDto> Handle(UpdateAccountGroupQuery message)
        {
            var accountGroup = await _context.AccountGroups.FindAsync(message.Id);
            if (accountGroup == null)
            {
                throw new Exception("Account group does not exist");
            }
            if (accountGroup.ParentId != message.ParentId)
            {
                throw new Exception("Cannot update Parent Id");
            }
            if (await AccountGroupExistsInOrganization(message, accountGroup.Name))
            {
                throw new Exception("Account Group with same name already exists under this parent");
            }
            accountGroup.DisplayName = message.DisplayName;
            accountGroup.Name = message.Name;
            await _context.SaveChangesAsync();
            return accountGroup.MapTo<AccountGroupDto>();
        }

        private async Task<bool> AccountGroupExistsInOrganization
            (AddAccountGroupQuery message, string oldName)
        {
            return await _context.AccountGroups
                .AnyAsync(c => c.ParentId == message.ParentId &&
                               c.OrganizationId == message.OrganizationId &&
                               !c.IsDeleted &&
                               !c.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase) &&
                               c.Name.Equals(message.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
