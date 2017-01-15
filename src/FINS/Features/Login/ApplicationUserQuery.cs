using FINS.Models;
using MediatR;

namespace FINS.Features.Login
{
    public class ApplicationUserQuery : IRequest<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
