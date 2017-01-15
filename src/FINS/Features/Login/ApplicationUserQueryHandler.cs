using System.Threading.Tasks;
using FINS.Context;
using FINS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Login
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
                .AsNoTracking()
                .Include(a => a.Claims)
                .Include(a => a.Roles)
                .SingleOrDefaultAsync(a => a.NormalizedUserName == normalizedUserName);
             return user;
        }
    }
}