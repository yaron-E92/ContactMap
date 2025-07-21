using Microsoft.EntityFrameworkCore;
using ContactMap.Infrastructure.Persistence;
using ContactMap.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata;
using ContactMap.Domain.Entities;

namespace ContactMap.Infrastructure.Tests.Persistence;

[TestFixture]
public class ContactMapDbContextTests
{
    private DbContextOptions<ContactMapDbContext> _options = null!;
    private ContactMapDbContext _context = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ContactMapDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;
        _context = new ContactMapDbContext(_options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void Address_ShouldBe_Owned_ByPerson()
    {
        // Act
        IEntityType? addressEntityType = _context.Model.FindEntityType(typeof(Address));
        IEntityType? ownerType = addressEntityType?.FindOwnership()?.PrincipalEntityType;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(addressEntityType, Is.Not.Null, "Address entity type should exist in the model.");
            Assert.That(ownerType, Is.Not.Null, "Address should be owned by a parent entity type.");
            Assert.That(ownerType?.ClrType, Is.EqualTo(typeof(Person)), "Address should be owned by Person entity type.");
        }
    }

    [Test]
    public void ContactDetails_ShouldBe_Owned_ByPerson()
    {
        // Act
        IEntityType? contactDetailsEntityType = _context.Model.FindEntityType(typeof(ContactDetails));
        IEntityType? ownerType = contactDetailsEntityType?.FindOwnership()?.PrincipalEntityType;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contactDetailsEntityType, Is.Not.Null, "ContactDetails entity type should exist in the model.");
            Assert.That(ownerType, Is.Not.Null, "ContactDetails should be owned by a parent entity type.");
            Assert.That(ownerType?.ClrType, Is.EqualTo(typeof(Person)), "ContactDetails should be owned by Person entity type.");
        }
    }
}
