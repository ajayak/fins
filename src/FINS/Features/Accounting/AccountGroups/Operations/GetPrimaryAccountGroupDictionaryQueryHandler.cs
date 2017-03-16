using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class GetPrimaryAccountGroupDictionaryQueryHandler :
        IAsyncRequestHandler<GetPrimaryAccountGroupDictionaryQuery, Dictionary<int, string>>
    {
        private readonly FinsDbContext _context;

        public GetPrimaryAccountGroupDictionaryQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets Account Groups dictionary for Organization except deleted
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, string>> Handle(GetPrimaryAccountGroupDictionaryQuery message)
        {
            return await _context.AccountGroups
                .AsNoTracking()
                .Where(c => c.OrganizationId == message.OrganizationId && !c.IsDeleted && c.IsPrimary)
                .ToDictionaryAsync(c => c.Id, c => c.Name);
        }
    }
}
