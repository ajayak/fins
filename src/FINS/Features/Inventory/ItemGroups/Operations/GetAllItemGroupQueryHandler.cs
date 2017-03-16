using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.ItemGroups.Operations
{
    public class GetAllItemGroupQueryHandler : IAsyncRequestHandler<GetAllItemGroupQuery, List<ItemGroupDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllItemGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Item Groups for Organization except deleted
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<List<ItemGroupDto>> Handle(GetAllItemGroupQuery message)
        {
            return await _context.ItemGroups
                .AsNoTracking()
                .Where(c => c.OrganizationId == message.OrganizationId && !c.IsDeleted)
                .ProjectTo<ItemGroupDto>()
                .ToListAsync();
        }
    }
}
