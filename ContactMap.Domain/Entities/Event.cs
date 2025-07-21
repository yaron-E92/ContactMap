using ContactMap.Domain.Common;

namespace ContactMap.Domain.Entities;

/// <summary>
/// Represents an event in the contact map system.
/// </summary>
public class Event : Entity
{
    /// <summary>
    /// Gets or sets the name of the event.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date of the event.
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Gets or sets the attendees of the event.
    /// </summary>
    public ICollection<Person> Attendees { get; set; } = [];
}
