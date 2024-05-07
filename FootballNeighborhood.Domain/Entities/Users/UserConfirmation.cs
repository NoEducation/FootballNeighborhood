using FootballNeighborhood.Domain.Entities.Common;

namespace FootballNeighborhood.Domain.Entities.Users;

public class UserConfirmation : EntityWithAdditionalUserInfo
{
    public string Code { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? UsedDate { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}

