using ContactMap.Domain.Common;

namespace ContactMap.Domain.ValueObjects;

/// <summary>
/// Represents a postal address value object.
/// </summary>
public class Address : ValueObject
{
    /// <summary>
    /// The street address.
    /// </summary>
    public string Street { get; set; } = string.Empty;
    /// <summary>
    /// The city.
    /// </summary>
    public string City { get; set; } = string.Empty;
    /// <summary>
    /// The state or region.
    /// </summary>
    public string State { get; set; } = string.Empty;
    /// <summary>
    /// The country.
    /// </summary>
    public string Country { get; set; } = string.Empty;
    /// <summary>
    /// The postal code.
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return PostalCode;
    }
}
