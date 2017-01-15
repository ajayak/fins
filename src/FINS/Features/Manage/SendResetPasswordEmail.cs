using MediatR;

namespace FINS.Features.Manage
{
    public class SendResetPasswordEmail : IRequest
    {
        public string Email { get; set; }
        public string CallbackUrl { get; set; }
    }
}
