using ContactMap.Domain.Common;

namespace ContactMap.Domain.Entities;

/// <summary>
/// Represents a relationship between two people in the contact map system.
/// </summary>
public class Relationship : Entity
{
    /// <summary>
    /// Gets or sets the ID of the person who requested the relationship.
    /// </summary>
    public Guid RequesterId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the person who is the addressee of the relationship.
    /// </summary>
    public Guid AddresseeId { get; set; }
    /// <summary>
    /// Gets or sets the status of the relationship.
    /// </summary>
    public RelationshipStatus Status { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the relationship was requested.
    /// </summary>
    public DateTime RequestedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the relationship was responded to, if any.
    /// </summary>
    public DateTime? RespondedAt { get; set; }
}
