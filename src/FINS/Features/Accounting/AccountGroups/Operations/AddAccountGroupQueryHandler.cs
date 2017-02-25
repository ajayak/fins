using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.AutoMap;
using FINS.Context;
using FINS.Models.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AddAccountGroupQueryHandler : IAsyncRequestHandler<AddAccountGroupQuery, AccountGroupDto>
    {
        private readonly FinsDbContext _context;

        public AddAccountGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new Account group
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AccountGroupDto> Handle(AddAccountGroupQuery message)
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

            var accountGroup = message.MapTo<AccountGroup>();
            await _context.AccountGroups.AddAsync(accountGroup);

            await _context.SaveChangesAsync();
            return accountGroup.MapTo<AccountGroupDto>();
        }

        private bool CheckParentOrganizationIdExists(int parentId)
        {
            if (parentId == 0) return true;
            return _context.AccountGroups.Any(c => c.Id == parentId);
        }
    }
}
