
using FootballNeighborhood.Domain.Dtos.Emails;

namespace FootballNeighborhood.Services.Emails;

public interface IEmailService
{
    Task SendEmailAsync(MessageInfoDto email);
}