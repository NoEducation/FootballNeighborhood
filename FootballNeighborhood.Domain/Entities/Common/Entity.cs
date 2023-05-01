namespace FootballNeighborhood.Domain.Entities.Common;

public abstract class Entity : Entity<int>
{
}

public abstract class Entity<T>
{
    public T Id { get; } = default!;
}