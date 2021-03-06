﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Common.State.Operations
{
    public class GetAllStatesQueryHandler : IAsyncRequestHandler<GetAllStatesQuery, List<NameCodeDto<int>>>
    {
        private readonly FinsDbContext _context;

        public GetAllStatesQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<List<NameCodeDto<int>>> Handle(GetAllStatesQuery query)
        {
            return await _context.States
                .Where(c => c.OrganizationId == query.OrganizationId && !c.IsDeleted)
                .Select(c => new NameCodeDto<int>()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code
                })
                .ToListAsync();
        }
    }
}
