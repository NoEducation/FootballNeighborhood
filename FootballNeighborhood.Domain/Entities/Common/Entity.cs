using System.ComponentModel.DataAnnotations;

namespace FootballNeighborhood.Domain.Entities.Common;

public abstract class Entity : Entity<int>
{
}

public abstract class Entity<T>
{
    [Key] public T Id { get; protected set; } = default!;
}