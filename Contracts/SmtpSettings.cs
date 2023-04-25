namespace Contracts;

public class SmtpSettings
{
    public SmtpSettings(string hostServer, string authUserName, string authPassword, bool useSsl = false,
        int serverPort = 25)
    {
        HostServer = hostServer;
        AuthUserName = authUserName;
        AuthPassword = authPassword;
        UseSsl = useSsl;
        ServerPort = serverPort;
    }

    public string HostServer { get; set; }
    public string AuthUserName { get; set; }
    public string AuthPassword { get; set; }
    public bool UseSsl { get; set; }
    public int ServerPort { get; set; }
}