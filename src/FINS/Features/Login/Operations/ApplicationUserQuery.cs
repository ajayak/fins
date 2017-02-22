using FINS.Models;
using FINS.Models.App;
using MediatR;

namespace FINS.Features.Login.Operations
{
    public class ApplicationUserQuery : IRequest<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
