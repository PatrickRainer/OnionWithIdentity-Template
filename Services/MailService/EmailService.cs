using System.Threading.Tasks;
using Contracts;
using MailKit.Net.Smtp;
using MimeKit;
using Services.Abstractions.MailService;

namespace Services.MailService;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(MessageSettings messageSettings, SmtpSettings smtpSettings)
    {
        // Build Message
        var mimeMessage = new MimeMessage();

        var from = new MailboxAddress(messageSettings.FromName,
            messageSettings.FromEmail);
        mimeMessage.From.Add(from);

        var to = new MailboxAddress(messageSettings.ToName, messageSettings.ToEmail);
        mimeMessage.To.Add(to);

        mimeMessage.Subject = messageSettings.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = messageSettings.Message
        };
        mimeMessage.Body = bodyBuilder.ToMessageBody();


        // Send Message
        var client = new SmtpClient();
        await client.ConnectAsync(smtpSettings.HostServer, smtpSettings.ServerPort, smtpSettings.UseSsl);
        await client.AuthenticateAsync(smtpSettings.AuthUserName, smtpSettings.AuthPassword);
        await client.SendAsync(mimeMessage);

        //Disconnect
        await client.DisconnectAsync(true);
        client.Dispose();
    }
}