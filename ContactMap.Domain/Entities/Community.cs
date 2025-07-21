using ContactMap.Domain.Common;

namespace ContactMap.Domain.Entities;

/// <summary>
/// Represents a community in the contact map system.
/// </summary>
public class Community : Entity
{
    /// <summary>
    /// Gets or sets the name of the community.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the members of the community.
    /// </summary>
    public ICollection<Person> Members { get; set; } = [];
}
