using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AccountGroupExistsInOrganizationQueryHandler :
        IAsyncRequestHandler<AccountGroupExistsInOrganizationQuery, bool>
    {
        private readonly FinsDbContext _context;

        public AccountGroupExistsInOrganizationQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AccountGroupExistsInOrganizationQuery message)
        {
            return await _context.AccountGroups
                .AnyAsync(c => c.ParentId == message.ParentAccountGroupId &&
                               c.OrganizationId == message.OrganizationId &&
                               !c.IsDeleted &&
                               c.Name.Equals(message.AccountGroupName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
