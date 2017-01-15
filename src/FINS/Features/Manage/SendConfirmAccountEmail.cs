using MediatR;

namespace FINS.Features.Manage
{
    public class SendConfirmAccountEmail : IRequest
    {
        public string Email { get; set; }
        public string CallbackUrl { get; set; }
    }
}
