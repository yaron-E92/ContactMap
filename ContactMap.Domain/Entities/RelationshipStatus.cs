namespace ContactMap.Domain.Entities;

/// <summary>
/// Represents the status of a relationship.
/// </summary>
public enum RelationshipStatus
{
    /// <summary>
    /// The relationship is pending approval.
    /// </summary>
    Pending,
    /// <summary>
    /// The relationship has been approved.
    /// </summary>
    Approved,
    /// <summary>
    /// The relationship has been rejected.
    /// </summary>
    Rejected
}
