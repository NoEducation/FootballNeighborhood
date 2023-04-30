namespace FootballNeighborhood.Domain.Entities.Common;

public abstract class EntityWithAdditionalUserInfo : Entity<int>
{
    public int? AddedByUserId { get; private set; }
    public DateTimeOffset AddedDate { get; private set; }
    public int? ModifiedByUserId { get; private set; }
    public DateTimeOffset? ModifiedDate { get; private set; }

    protected void SetModificationInfo(int modifiedByUserId, DateTimeOffset? modificationDateTime = null)
    {
        ModifiedByUserId = modifiedByUserId;
        ModifiedDate = modificationDateTime ?? DateTimeOffset.Now;
    }

    protected void SetAdditionInfo(int? addedByUserId = null, DateTimeOffset? addedDate = null)
    {
        AddedByUserId = addedByUserId;
        AddedDate = addedDate ?? DateTimeOffset.Now;
    }
}