namespace FootballNeighborhood.Domain.Entities.Common;

public abstract class EntityWithAdditionalUserInfo : EntityWithAdditionalUserInfo<int>
{
}

public abstract class EntityWithAdditionalUserInfo<T> : Entity<T>
{
    public int? AddedByUserId { get; private set; }
    public DateTimeOffset AddedDate { get; private set; }
    public int? ModifiedByUserId { get; private set; }
    public DateTimeOffset? ModifiedDate { get; private set; }

    public void SetModificationInfo(int modifiedByUserId, DateTimeOffset? modificationDateTime = null)
    {
        ModifiedByUserId = modifiedByUserId;
        ModifiedDate = modificationDateTime ?? DateTimeOffset.Now;
    }

    public void SetAdditionInfo(int? addedByUserId = null, DateTimeOffset? addedDate = null)
    {
        AddedByUserId = addedByUserId;
        AddedDate = addedDate ?? DateTimeOffset.Now;
    }
}