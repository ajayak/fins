using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Common.State.Operations
{
    public class GetAllStatesQueryHandler : IAsyncRequestHandler<GetAllStatesQuery, List<StateDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllStatesQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<List<StateDto>> Handle(GetAllStatesQuery query)
        {
            return await _context.States
                .Where(c => c.OrganizationId == query.OrganizationId && !c.IsDeleted)
                .ProjectTo<StateDto>()
                .ToListAsync();
        }
    }
}
