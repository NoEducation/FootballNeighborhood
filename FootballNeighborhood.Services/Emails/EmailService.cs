using System.Net;
using FootballNeighborhood.Domain.Dtos.Emails;
using FootballNeighborhood.Domain.Options;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace FootballNeighborhood.Services.Emails;

public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;

    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(MessageInfoDto email)
    {
        MailMessage mail = new MailMessage();

        using var smtpClient = GetSmtpClient();
        using var mailMessage = PrepareMailMessage(email);

        await smtpClient.SendMailAsync(mail);
    }

    private SmtpClient GetSmtpClient()
    {
        var smtpServer = new SmtpClient(_emailOptions.SmtpServer);

        smtpServer.Port = _emailOptions.Port;
        smtpServer.Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password);
        smtpServer.EnableSsl = true;

        return smtpServer;
    }

    private MailMessage PrepareMailMessage(MessageInfoDto email)
    {
        var mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(_emailOptions.FromAddress);

        foreach (var to in email.Tos) mailMessage.To.Add(to);

        mailMessage.Subject = email.Title;
        mailMessage.Body = email.Message;

        AddAttachments(email, mailMessage);

        return mailMessage;
    }

    private void AddAttachments(MessageInfoDto email, MailMessage mailMessage)
    {
        if (email.Attachments is null || !email.Attachments.Any()) return;
        
        foreach (var attachment in email.Attachments!)
            mailMessage.Attachments.Add(new Attachment(attachment.Stream,attachment.Name));
    }
}
