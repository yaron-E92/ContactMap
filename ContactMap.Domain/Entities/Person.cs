using ContactMap.Domain.ValueObjects;
using ContactMap.Domain.Common;

namespace ContactMap.Domain.Entities;

/// <summary>
/// Represents a person in the contact map system.
/// </summary>
public class Person : Entity
{
    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the contact details of the person.
    /// </summary>
    public ContactDetails ContactDetails { get; set; } = null!;
    /// <summary>
    /// Gets or sets the address of the person.
    /// </summary>
    public Address Address { get; set; } = null!;
    /// <summary>
    /// Gets or sets the communities the person belongs to.
    /// </summary>
    public ICollection<Community> Communities { get; set; } = [];
    /// <summary>
    /// Gets or sets the events the person is attending.
    /// </summary>
    public ICollection<Event> Events { get; set; } = [];
}
