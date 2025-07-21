using ContactMap.Domain.ValueObjects;

namespace ContactMap.Domain.Tests;

[TestFixture]
public class ValueObjectTests
{
    [Test]
    public void Address_Equality_Works()
    {
        var a1 = new Address { Street = "1", City = "C", State = "S", Country = "X", PostalCode = "000" };
        var a2 = new Address { Street = "1", City = "C", State = "S", Country = "X", PostalCode = "000" };
        Assert.That(a1, Is.EqualTo(a2));
    }

    [Test]
    public void Address_Inequality_Works()
    {
        var a1 = new Address { Street = "1", City = "C", State = "S", Country = "X", PostalCode = "000" };
        var a2 = new Address { Street = "2", City = "C", State = "S", Country = "X", PostalCode = "000" };
        Assert.That(a1, Is.Not.EqualTo(a2));
    }

    [Test]
    public void ContactDetails_Equality_Works()
    {
        var c1 = new ContactDetails { Phone = "123", Email = "a@b.com", Social = "@x", PhotoUrl = "url" };
        var c2 = new ContactDetails { Phone = "123", Email = "a@b.com", Social = "@x", PhotoUrl = "url" };
        Assert.That(c1, Is.EqualTo(c2));
    }
}
