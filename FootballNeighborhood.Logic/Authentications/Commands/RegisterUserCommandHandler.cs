using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Dtos.Emails;
using FootballNeighborhood.Domain.Entities.Users;
using FootballNeighborhood.Domain.Enums.Emails;
using FootballNeighborhood.Domain.Enums.Roles;
using FootballNeighborhood.Domain.Options;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Authentications;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.Emails;
using System.Net;
using System.Security.Cryptography;

namespace FootballNeighborhood.Logic.Authentications.Commands;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, SuccessMessageAndObjectId>
{
    private readonly Context _context;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly FrontendOptions _frontendOptions;

    public RegisterUserCommandHandler(Context context,
        IPasswordHasherService passwordHasherService,
        IEmailService emailService,
        FrontendOptions frontendOptions,
        IEmailTemplateService emailTemplateService)
    {
        _context = context;
        _passwordHasherService = passwordHasherService;
        _emailService = emailService;
        _frontendOptions = frontendOptions;
        _emailTemplateService = emailTemplateService;
    }

    public async Task<OperationResult<SuccessMessageAndObjectId>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessageAndObjectId>();
        var user = AddUser(request);
        var userConfirmation = AddUserConfirmation(user);

        await _context.SaveChangesAsync(cancellationToken);

        await SendNotification(userConfirmation);

        result.Result = new SuccessMessageAndObjectId
        {
            ObjectId = user.Id,
            Message = AuthenticationsRescource.UserRegistered_SuccessMessage
        };

        return result;
    }

    private User AddUser(RegisterUserCommand request)
    {
        var user = new User
        {
            RoleId = (int)request.Role,
            Login = request.Login,
            Password = _passwordHasherService.GenerateHash(request.Password),
            Email = request.Email,
            IsActive = request.Role == Roles.Player,
            IsConfirmed = false
        };

        _context.Users.Attach(user);
        return user;
    }

    private UserConfirmation AddUserConfirmation(User user)
    {
        var confirmationUser = new UserConfirmation
        {
            User = user,
            Code = WebUtility.UrlEncode(Convert.ToBase64String(RandomNumberGenerator.GetBytes(128))),
            CreatedDate = DateTime.UtcNow,
            IsUsed = false,
            UsedDate = null
        };

        _context.Attach(confirmationUser);

        return confirmationUser;
    }

    private async Task SendNotification(UserConfirmation userConfirmation)
    {
        var message = await CreateConfirmationMessage(userConfirmation);

        await _emailService.SendEmailAsync(message);
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
            Title = "",
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