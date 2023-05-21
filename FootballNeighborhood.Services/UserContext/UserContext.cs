using System.Security.Claims;
using FootballNeighborhood.Domain.Enums.Roles;
using FootballNeighborhood.Services.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Services.UserContext;

public class UserContext : IUserContext
{
    private readonly Context _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private List<string>? _userPermissions;

    public UserContext(IHttpContextAccessor httpContextAccessor,
        Context context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;

        SetCurrentUserId();
        SetRole();
    }

    public int CurrentUserId { get; private set; }
    public Roles Role { get; private set; }

    public async Task<bool> UserHasPermission(string target)
    {
        if (_userPermissions is null) await LoadPermissions();

        return _userPermissions!.Any(permission => permission == target);
    }

    private async Task LoadPermissions()
    {
        _userPermissions = await _context.Permissions
            .Include(permission => permission.Roles)
            .Where(permission => permission.Roles!.Any(role => role.Id == (int)Role))
            .Select(permission => permission.Name)
            .Distinct()
            .ToListAsync();
    }

    private void SetCurrentUserId()
    {
        var currentUserId =
            _httpContextAccessor.HttpContext.User.Claims
                .Single(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        CurrentUserId = int.Parse(currentUserId);
    }

    private void SetRole()
    {
        var role =
            _httpContextAccessor.HttpContext.User.Claims
                .Single(claim => claim.Type == ClaimTypes.Role).Value;

        Role = (Roles)int.Parse(role);
    }
}