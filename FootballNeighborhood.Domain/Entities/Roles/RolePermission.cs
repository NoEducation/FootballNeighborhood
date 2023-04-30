using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Permissions;

namespace FootballNeighborhood.Domain.Entities.Roles;

public class RolePermission : Entity<int>
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Permission? Permission { get; set; }
}