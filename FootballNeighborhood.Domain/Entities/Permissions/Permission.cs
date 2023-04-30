using FootballNeighborhood.Domain.Entities.Common;

namespace FootballNeighborhood.Domain.Entities.Permissions;

public class Permission : Entity<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}