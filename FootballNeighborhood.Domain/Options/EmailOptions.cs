namespace FootballNeighborhood.Domain.Options;

public class EmailOptions
{
    public static string Key => "EmailOptions";

    public string FromAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int Port { get; set; } = default!;
    public int Ssl { get; set; } = default!;
    public string SmtpServer { get; set; } = default!;
    public string EmailTemplatePath { get; set; } = default!;
}
