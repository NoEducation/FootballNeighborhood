using FootballNeighborhood.Domain.Dtos.Emails;
using FootballNeighborhood.Domain.Enums.Emails;
using FootballNeighborhood.Domain.Options;
using Microsoft.Extensions.Options;
using System.Text;

namespace FootballNeighborhood.Services.Emails
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly EmailOptions _emailOptions;

        private readonly Dictionary<EmailTypeEnum, string> _emailTemplateNameByEmailType = new()
        {
            { EmailTypeEnum.ConfirmationUser, "defaultTemplate.html" }
        };

        public EmailTemplateService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task<string> GenerateMessageBodyForEmailType(EmailTypeEnum emailType,
            List<EmailTranslationDto> emailTranslations)
        {
            if (!_emailTemplateNameByEmailType.TryGetValue(emailType, out var templateName))
                throw new ArgumentException($"There is no defined email template name for: {emailType}");

            using var streamReader = new StreamReader(Path.Combine(_emailOptions.EmailTemplatePath, templateName!), Encoding.UTF8);

            var htmlBody = await streamReader.ReadToEndAsync();

            return ReplaceContent(htmlBody, emailTranslations);
        }

        private string ReplaceContent(string source, List<EmailTranslationDto> emailTranslations)
        {
            foreach (var emailTranslation in emailTranslations)
                source = source.Replace("{{" + emailTranslation.Token + "}}", emailTranslation.TranslationValue);

            return source;
        }
    }
}

