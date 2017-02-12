using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Login
{
    public class OrganizationExistsQueryHandler : IAsyncRequestHandler<OrganizationExistsQuery, bool>
    {
        private readonly FinsDbContext _dataContext;

        public OrganizationExistsQueryHandler(FinsDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<bool> Handle(OrganizationExistsQuery message)
        {
            return await _dataContext.Organizations
                .AnyAsync(c => c.Name.Equals(message.OrganizationName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
