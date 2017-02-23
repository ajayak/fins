using System.Threading.Tasks;
using FINS.Context;
using FINS.Models.App;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Login.Operations
{
    public class ApplicationUserQueryHandler : IAsyncRequestHandler<ApplicationUserQuery, ApplicationUser>
    {
        private readonly FinsDbContext _context;

        public ApplicationUserQueryHandler(FinsDbContext context)
        {
            _context = context;

        }
        public async Task<ApplicationUser> Handle(ApplicationUserQuery message)
        {
            var normalizedUserName = message.UserName.ToUpperInvariant();
            var user = await _context.Users
                .Include(a => a.Claims)
                .Include(a => a.Roles)
                .FirstOrDefaultAsync(a => a.NormalizedUserName == normalizedUserName);
             return user;
        }
    }
}