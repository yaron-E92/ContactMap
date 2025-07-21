namespace ContactMap.Domain.Common;

/// <summary>
/// Represents a base entity with an identity.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is Entity other
            && (ReferenceEquals(this, other)
                || (GetType() == other.GetType() && Id == other.Id));
    }

    public static bool operator ==(Entity a, Entity b)
    {
        return (a is null && b is null) || (a is not null && b is not null && a.Equals(b));
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns a hash code for the entity.
    /// </summary>
    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}
