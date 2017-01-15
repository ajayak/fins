using System.Threading.Tasks;
using FINS.Services;
using MediatR;

namespace FINS.Features.Manage
{
    public class SendConfirmAccountEmailHandler : IAsyncRequestHandler<SendConfirmAccountEmail>
    {
        private readonly IEmailSender _emailSender;
       
        public SendConfirmAccountEmailHandler(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }
        
        public async Task Handle(SendConfirmAccountEmail message)
        {
            await _emailSender.SendEmailAsync(message.Email, "Confirm your allReady account",
                $"Please confirm your allReady account by clicking this link: <a href=\"{message.CallbackUrl}\">{message.CallbackUrl}</a>");
        }
    }
}
