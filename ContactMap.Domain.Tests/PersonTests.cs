using ContactMap.Domain.Entities;
using ContactMap.Domain.ValueObjects;

namespace ContactMap.Domain.Tests;

[TestFixture]
public class PersonTests
{
    [Test]
    public void Can_Construct_Person_With_Properties()
    {
        var address = new Address { Street = "123 Main St", City = "Testville", State = "TS", Country = "Testland", PostalCode = "12345" };
        var contact = new ContactDetails { Phone = "1234567890", Email = "test@example.com" };
        var person = new Person
        {
            Name = "John Doe",
            Address = address,
            ContactDetails = contact,
            Communities = [],
            Events = []
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(person.Name, Is.EqualTo("John Doe"));
            Assert.That(person.Address, Is.EqualTo(address));
            Assert.That(person.ContactDetails, Is.EqualTo(contact));
        }
    }

    [Test]
    public void Person_Equality_Based_On_Id()
    {
        var p1 = new Person { Name = "A" };
        var p2 = new Person { Name = "A" };
        p2.GetType().GetProperty("Id")!.SetValue(p2, p1.Id); // forcibly set same Id for test
        Assert.That(p1, Is.EqualTo(p2));
    }

    [Test]
    public void Person_Not_Equal_If_Different_Id()
    {
        var p1 = new Person { Name = "A" };
        var p2 = new Person { Name = "A" };
        Assert.That(p1, Is.Not.EqualTo(p2));
    }
}
