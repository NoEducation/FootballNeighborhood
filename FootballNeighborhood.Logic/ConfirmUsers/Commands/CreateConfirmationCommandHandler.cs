using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Emails;
using FootballNeighborhood.Domain.Entities.Users;
using FootballNeighborhood.Domain.Enums.Emails;
using FootballNeighborhood.Domain.Options;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.ConfirmUsers.Queries;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.Emails;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;

namespace FootballNeighborhood.Logic.ConfirmUsers.Commands;

public class CreateConfirmationCommandHandler : ICommandHandler<CreateConfirmationCommand, SuccessMessage>
{
    private readonly FrontendOptions _frontendOptions;
    private readonly Context _context;
    private readonly IDispatcher _dispatcher;
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _emailTemplateService;

    public CreateConfirmationCommandHandler(Context context,
        IDispatcher dispatcher,
        IEmailService emailService,
        IEmailTemplateService emailTemplateService,
        IOptions<FrontendOptions> frontendOptions)
    {
        _context = context;
        _dispatcher = dispatcher;
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
        _frontendOptions = frontendOptions.Value;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(CreateConfirmationCommand request, CancellationToken cancellationToken)
    {
        var result = await ValidateUserHasActiveConfirmation(request);

        if (!result.Success) return result;

        var user = await _context.Users
            .SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            result.AddError(AuthenticationsRescource.UserWithIdDoesNotExists_ErrorMessage);
            return result;
        }

        if (user.IsConfirmed)
        {
            result.AddError(UserConfirmationsResource.UserAlreadyConfirmed_ErrorMessage);
            return result;
        }

        var confirmationUser = CreateConfirmation(user);

        await SendMessage(confirmationUser, result);

        if (!result.Success) return result;

        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessage()
        {
            Message = UserConfirmationsResource.UserConfirmationHasBeenSendAgain_SuccessMessage
        };

        return result;
    }

    private UserConfirmation CreateConfirmation(User? user)
    {
        var confirmationUser = new UserConfirmation
        {
            User = user,
            Code = WebUtility.UrlEncode(Convert.ToBase64String(RandomNumberGenerator.GetBytes(128))),
            IsUsed = false,
            UsedDate = null
        };

        confirmationUser.SetAdditionInfo();

        _context.Attach(confirmationUser);
        return confirmationUser;
    }

    private async Task<OperationResult<SuccessMessage>> ValidateUserHasActiveConfirmation(CreateConfirmationCommand request)
    {
        var result = new OperationResult<SuccessMessage>();

        var userHasActiveConfirmation = await _dispatcher.SendAsync(
            new CheckUserHasActiveConfirmationQuery
            {
                UserId = request.UserId
            });

        if (!userHasActiveConfirmation.Success)
        {
            result.AddError(userHasActiveConfirmation.Error!);
            return result;
        }

        if (userHasActiveConfirmation.Result!.IsConfirmationActive)
            result.AddError(UserConfirmationsResource.UserHasActiveConfirmation_ErrorMessage);

        return result;
    }

    private async Task SendMessage(UserConfirmation userConfirmation, OperationResult<SuccessMessage> result)
    {
        var message = await CreateConfirmationMessage(userConfirmation);

        try
        {
            await _emailService.SendEmailAsync(message);
        }
        catch (Exception)
        {
            result.AddError(UserConfirmationsResource.EmailCannotBeSend_ErrorMessage);
        }
    }

    private async Task<MessageInfoDto> CreateConfirmationMessage(UserConfirmation userConfirmation)
    {
        var uri = $"{_frontendOptions.Url}/confirmation-user?userId={userConfirmation.User!.Id}&code={userConfirmation.Code}";

        var translations = GetEmailTranslations(userConfirmation.User.Email!, uri);

        var htmlBody = await _emailTemplateService
            .GenerateMessageBodyForEmailType(EmailTypeEnum.ConfirmationUser, translations);

        var message = new MessageInfoDto
        {
            Tos = new List<string> { userConfirmation.User.Email! },
            Title = UserConfirmationsResource.UserConfirmationEmailTitle,
            Message = htmlBody
        };

        return message;
    }

    private List<EmailTranslationDto> GetEmailTranslations(string email, string uriAction)
    {
        return new List<EmailTranslationDto>()
        {
            new EmailTranslationDto()
            {
                Token = "Description",
                TranslationValue = string.Format(EmailsRescource.ConfirmUser_Description, email)
            },
            new EmailTranslationDto()
            {
                Token = "Action",
                TranslationValue = EmailsRescource.ConfirmUser_Action
            },
            new EmailTranslationDto()
            {
                Token = "ActionUrl",
                TranslationValue = uriAction
            },
            new EmailTranslationDto()
            {
                Token = "AdditionalInfo",
                TranslationValue = EmailsRescource.ConfirmUser_AdditionalInfo
            },
            new EmailTranslationDto()
            {
                Token = "Year",
                TranslationValue = DateTime.Now.Year.ToString()
            }
        };
    }
}

