using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class GetAllAccountGroupQueryHandler : IAsyncRequestHandler<GetAllAccountGroupQuery, List<AccountGroupDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllAccountGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Account Groups for Organization except deleted
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<List<AccountGroupDto>> Handle(GetAllAccountGroupQuery message)
        {
            return await _context.AccountGroups
                .AsNoTracking()
                .Where(c => c.OrganizationId == message.OrganizationId && !c.IsDeleted)
                .ProjectTo<AccountGroupDto>()
                .ToListAsync();
        }
    }
}
