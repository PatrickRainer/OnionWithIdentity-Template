using System.Threading.Tasks;
using Contracts;

namespace Services.Abstractions.MailService;

    public interface IEmailService
    {
        Task SendEmailAsync(MessageSettings messageSettings, SmtpSettings smtpSettings);
    }

    