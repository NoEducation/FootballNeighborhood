using FootballNeighborhood.Domain.Dtos.Emails;
using FootballNeighborhood.Domain.Enums.Emails;

namespace FootballNeighborhood.Services.Emails;

public interface IEmailTemplateService
{
    Task<string> GenerateMessageBodyForEmailType(
        EmailTypeEnum emailType, 
        List<EmailTranslationDto> emailTranslations); 
}

