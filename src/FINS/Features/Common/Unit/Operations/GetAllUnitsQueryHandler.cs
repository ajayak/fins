using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Common.Unit.Operations
{
    public class GetAllUnitsQueryHandler : IAsyncRequestHandler<GetAllUnitsQuery, List<NameCodeDto<int>>>
    {
        private readonly FinsDbContext _context;

        public GetAllUnitsQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<List<NameCodeDto<int>>> Handle(GetAllUnitsQuery query)
        {
            return await _context.Units
                .Where(c => c.OrganizationId == query.OrganizationId && !c.IsDeleted)
                .Select(c => new NameCodeDto<int>
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code
                })
                .ToListAsync();
        }
    }
}
