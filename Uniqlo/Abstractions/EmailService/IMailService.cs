using Uniqlo.Helper.Email;

namespace Uniqlo.Abstractions.EmailService;

public interface IMailService   
{
    Task SendEmailAsync(MailRequest mailRequest);

}
