using ContactMap.Domain.Common;

namespace ContactMap.Domain.ValueObjects;

/// <summary>
/// Represents contact details for a person.
/// </summary>
public class ContactDetails : ValueObject
{
    /// <summary>
    /// The phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// The email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// The social media handle or link.
    /// </summary>
    public string? Social { get; set; }
    /// <summary>
    /// The photo URL.
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Phone;
        yield return Email;
        yield return Social ?? string.Empty;
        yield return PhotoUrl ?? string.Empty;
    }
}
