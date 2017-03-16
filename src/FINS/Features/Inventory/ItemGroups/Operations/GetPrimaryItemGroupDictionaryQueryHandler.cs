using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class GetPrimaryItemGroupDictionaryQueryHandler :
        IAsyncRequestHandler<GetPrimaryItemGroupDictionaryQuery, Dictionary<int, string>>
    {
        private readonly FinsDbContext _context;

        public GetPrimaryItemGroupDictionaryQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets Item Groups dictionary for Organization except deleted
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, string>> Handle(GetPrimaryItemGroupDictionaryQuery message)
        {
            return await _context.ItemGroups
                .AsNoTracking()
                .Where(c => c.OrganizationId == message.OrganizationId && !c.IsDeleted && c.IsPrimary)
                .ToDictionaryAsync(c => c.Id, c => c.Name);
        }
    }
}
