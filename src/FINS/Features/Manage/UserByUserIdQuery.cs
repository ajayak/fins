using FINS.Models;
using FINS.Models.App;
using MediatR;

namespace FINS.Features.Manage
{
    public class UserByUserIdQuery : IRequest<ApplicationUser>
    {
        public string UserId { get; set; }
    }
}
