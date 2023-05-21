using FootballNeighborhood.Domain.Dtos.Authentications;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.Authentications.Commands;
using FootballNeighborhood.Services.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballNeighborhood.Api.Controllers;

public class AuthenticationController : BaseController
{
    private readonly ILoginService _loginService;

    public AuthenticationController(IDispatcher dispatcher, ILoginService loginService) : base(dispatcher)
    {
        _loginService = loginService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<OperationResult<UserLoggedDto>> Login([FromBody] UserCredentialsDto credentials,
        CancellationToken cancellationToken)
    {
        var result = await _loginService.Login(credentials, cancellationToken);

        if (!result.Success) Response.StatusCode = StatusCodes.Status401Unauthorized;

        return result;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<OperationResult<SuccessMessageAndObjectId>> Register([FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }
}