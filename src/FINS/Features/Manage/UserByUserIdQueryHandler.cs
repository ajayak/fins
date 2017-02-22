using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models;
using FINS.Models.App;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Manage
{
    public class UserByUserIdQueryHandler : IAsyncRequestHandler<UserByUserIdQuery, ApplicationUser>
    {
        private readonly FinsDbContext _dataContext;

        public UserByUserIdQueryHandler(FinsDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<ApplicationUser> Handle(UserByUserIdQuery message)
        {
            return await _dataContext.Users
                .Where(u => u.Id == message.UserId)
                .Include(u => u.Claims)
                .SingleOrDefaultAsync();
        }
    }
}
