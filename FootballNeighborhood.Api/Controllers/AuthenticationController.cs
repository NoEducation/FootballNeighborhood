using FootballNeighborhood.Domain.Dtos.Authentications;
using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Logic.Authentications;
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
    public Task<OperationResult<UserLoggedDto>> Login([FromBody] UserCredentialsDto credentials,
        CancellationToken cancellationToken)
    {
        return _loginService.Login(credentials, cancellationToken);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<OperationResult<SuccessMessageAndObjectId>> Register([FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        return await DispatchAsync(command, cancellationToken);
    }
}