using ContactMap.Infrastructure.Persistence;
using ContactMap.Infrastructure.Repositories;
using ContactMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactMap.Infrastructure.Tests.Repositories;

[TestFixture]
public class RelationshipRepositoryTests
{
    private DbContextOptions<ContactMapDbContext> _options = null!;
    private ContactMapDbContext _context = null!;
    private RelationshipRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ContactMapDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;
        _context = new ContactMapDbContext(_options);
        _repository = new RelationshipRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddAsync_SavesRelationship_ToDatabase()
    {
        // Arrange
        Relationship relationship = new()
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };

        // Act
        await _repository.AddAsync(relationship);

        // Assert
        Relationship? saved = await _context.Relationships.FindAsync(relationship.Id);
        Assert.That(saved, Is.Not.Null);
        Assert.That(saved!.RequesterId, Is.EqualTo(relationship.RequesterId));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsRelationship_WhenFound()
    {
        // Arrange
        Relationship relationship = new()
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        await _context.Relationships.AddAsync(relationship);
        await _context.SaveChangesAsync();

        // Act
        Relationship? found = await _repository.GetByIdAsync(relationship.Id);

        // Assert
        Assert.That(found, Is.Not.Null);
        Assert.That(found!.Id, Is.EqualTo(relationship.Id));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Act
        Relationship? found = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.That(found, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_RemovesRelationship_WhenFound()
    {
        // Arrange
        Relationship relationship = new()
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        await _context.Relationships.AddAsync(relationship);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(relationship.Id);
        Relationship? found = await _context.Relationships.FindAsync(relationship.Id);

        // Assert
        Assert.That(found, Is.Null);
    }

    [Test]
    public async Task GetByPersonIdAsync_ReturnsRelationships_ForPerson()
    {
        // Arrange
        var personId = Guid.NewGuid();
        Relationship[] relationships =
        [
            new() { RequesterId = personId, AddresseeId = Guid.NewGuid(), Status = RelationshipStatus.Pending, RequestedAt = DateTime.UtcNow },
            new() { RequesterId = Guid.NewGuid(), AddresseeId = personId, Status = RelationshipStatus.Approved, RequestedAt = DateTime.UtcNow },
            new() { RequesterId = Guid.NewGuid(), AddresseeId = Guid.NewGuid(), Status = RelationshipStatus.Pending, RequestedAt = DateTime.UtcNow }
        ];
        await _context.Relationships.AddRangeAsync(relationships);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Relationship> found = await _repository.GetByPersonIdAsync(personId);

        // Assert
        Assert.That(found.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_UpdatesRelationship_WhenExists()
    {
        // Arrange
        Relationship relationship = new()
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        await _context.Relationships.AddAsync(relationship);
        await _context.SaveChangesAsync();

        // Act
        relationship.Status = RelationshipStatus.Approved;
        bool result = await _repository.UpdateAsync(relationship);

        // Assert
        Relationship? updated = await _context.Relationships.FindAsync(relationship.Id);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.True);
            Assert.That(updated!.Status, Is.EqualTo(RelationshipStatus.Approved));
        }
    }

    [Test]
    public async Task UpdateAsync_ReturnsFalse_WhenRelationshipNotFound()
    {
        // Arrange
        Relationship relationship = new()
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        // Not added to context

        // Act
        bool result = await _repository.UpdateAsync(relationship);

        // Assert
        Assert.That(result, Is.False);
    }
}
