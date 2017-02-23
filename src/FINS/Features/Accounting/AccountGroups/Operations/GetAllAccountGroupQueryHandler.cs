using System.Collections.Generic;
using System.Threading.Tasks;
using FINS.Context;
using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class GetAllAccountGroupQueryHandler : IAsyncRequestHandler<GetAllAccountGroupQuery, List<AccountGroupDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllAccountGroupQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AccountGroupDto>> Handle(GetAllAccountGroupQuery message)
        {
            return null;
            //return await _context.AccountGroups
            //    .AsNoTracking()
            //    .Where(c => c.OrganizationId == message.OrganizationId)
            //    .ToListAsync();
        }

    }
}
