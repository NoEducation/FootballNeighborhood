using FootballNeighborhood.Domain.Entities.Common;
using FootballNeighborhood.Domain.Entities.Permissions;

namespace FootballNeighborhood.Domain.Entities.Roles;

public class Role : Entity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public ICollection<Permission>? Permissions { get; set; }
}