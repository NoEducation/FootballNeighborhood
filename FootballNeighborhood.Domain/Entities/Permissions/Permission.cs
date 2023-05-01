using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Roles;

namespace FootballNeighborhood.Domain.Entities.Permissions;

public class Permission : Entity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }
}