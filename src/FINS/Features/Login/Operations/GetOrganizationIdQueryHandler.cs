using System;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Login.Operations
{
    public class GetOrganizationIdQueryHandler : IAsyncRequestHandler<GetOrganizationIdQuery, int?>
    {
        private readonly FinsDbContext _dataContext;

        public GetOrganizationIdQueryHandler(FinsDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<int?> Handle(GetOrganizationIdQuery message)
        {
            return await _dataContext.Organizations
                .Where(c => c.Name.Equals(message.OrganizationName, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
        }
    }
}
