using FootballNeighborhood.Services.UserContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FootballNeighborhood.Infrastructure.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionAuthorizationAttribute : ActionFilterAttribute
{
    private readonly string _permission;
    private readonly IUserContext _userContext;

    public PermissionAuthorizationAttribute(string permission, IUserContext userContext)
    {
        _permission = permission;
        _userContext = userContext;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasRequiredPermission = await _userContext.UserHasPermission(_permission);

        if (!hasRequiredPermission)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await next();
    }
}