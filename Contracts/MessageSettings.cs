namespace Contracts;

public class MessageSettings
{
    public MessageSettings(string toName, string toEmail, string subject, string message, string fromEmail,
        string fromName)
    {
        ToName = toName;
        ToEmail = toEmail;
        Subject = subject;
        Message = message;
        FromEmail = fromEmail;
        FromName = fromName;
    }

    public string ToName { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
}