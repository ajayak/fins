using System.Threading.Tasks;
using FINS.Services;
using MediatR;

namespace FINS.Features.Manage
{
    public class SendResetPasswordEmailHandler : IAsyncRequestHandler<SendResetPasswordEmail>
    {
        private readonly IEmailSender _emailSender;

        public SendResetPasswordEmailHandler(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        public async Task Handle(SendResetPasswordEmail message)
        {
            await _emailSender.SendEmailAsync(message.Email, "Reset allReady Password",
                $"Please reset your allReady password by clicking here: <a href=\"{message.CallbackUrl}\">link</a>");
        }
    }
}
