using ContactMap.Infrastructure.Persistence;
using ContactMap.Infrastructure.Repositories;
using ContactMap.Domain.Entities;
using ContactMap.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ContactMap.Infrastructure.Tests.Repositories;

[TestFixture]
public class PersonRepositoryTests
{
    private DbContextOptions<ContactMapDbContext> _options = null!;
    private ContactMapDbContext _context = null!;
    private PersonRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ContactMapDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;
        _context = new ContactMapDbContext(_options);
        _repository = new PersonRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddAsync_SavesPerson_ToDatabase()
    {
        // Arrange
        Person person = new()
        {
            Name = "New Person",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Test City",
                State = "TS",
                Country = "Test Country",
                PostalCode = "12345"
            },
            ContactDetails = new ContactDetails
            {
                Email = "test@example.com",
                Phone = "1234567890"
            }
        };

        // Act
        await _repository.AddAsync(person);

        // Assert
        Person? savedPerson = await _context.People
            .Include(p => p.Address)
            .Include(p => p.ContactDetails)
            .FirstOrDefaultAsync(p => p.Id == person.Id);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(savedPerson, Is.Not.Null);
            Assert.That(savedPerson!.Name, Is.EqualTo("New Person"));
            Assert.That(savedPerson.Address.City, Is.EqualTo("Test City"));
            Assert.That(savedPerson.ContactDetails.Email, Is.EqualTo("test@example.com"));
        }
    }

    [Test]
    public async Task UpdateAsync_ModifiesPerson_InDatabase()
    {
        // Arrange
        Person person = new()
        {
            Name = "Original Name",
            Address = new Address
            {
                Street = "Original St",
                City = "Original City",
                State = "OS",
                Country = "Original Country",
                PostalCode = "00000"
            }
        };
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();

        // Act
        person.Name = "Updated Name";
        person.Address = new Address
        {
            Street = "New St",
            City = "New City",
            State = "NS",
            Country = "New Country",
            PostalCode = "11111"
        };
        bool result = await _repository.UpdateAsync(person);

        // Assert
        Person updatedPerson = await _context.People
            .Include(p => p.Address)
            .FirstAsync(p => p.Id == person.Id);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.True);
            Assert.That(updatedPerson.Name, Is.EqualTo("Updated Name"));
            Assert.That(updatedPerson.Address.City, Is.EqualTo("New City"));
            Assert.That(updatedPerson.Address.PostalCode, Is.EqualTo("11111"));
        }
    }

    [Test]
    public async Task UpdateAsync_ReturnsFalse_WhenPersonNotFound()
    {
        // Arrange
        Person person = new() { Name = "Non-existent" };

        // Act
        bool result = await _repository.UpdateAsync(person);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetByIdAsync_ReturnsNull_WhenPersonNotFound()
    {
        Person? result = await _repository.GetByIdAsync(Guid.NewGuid());
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_ReturnsPerson_WhenFound()
    {
        // Arrange
        Person person = new() { Name = "Test Person" };
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();

        // Act
        Person? result = await _repository.GetByIdAsync(person.Id);

        // Assert
        Assert.That(result?.Name, Is.EqualTo(person.Name));
    }

    [Test]
    public async Task SearchAsync_ReturnsMatchingPeople_ByName()
    {
        // Arrange
        Person[] people =
        [
            new() { Name = "John Doe" },
            new() { Name = "Jane Doe" },
            new() { Name = "Alice Smith" }
        ];
        await _context.People.AddRangeAsync(people);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Person> results = await _repository.SearchAsync("Doe", null);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(results.Count(), Is.EqualTo(2));
            Assert.That(results.Select(p => p.Name), Is.EquivalentTo(new[] { "John Doe", "Jane Doe" }));
        }
    }

    [Test]
    public async Task SearchAsync_ReturnsAll_WhenNoFilters()
    {
        // Arrange
        Person[] people =
        [
            new() { Name = "John Doe" },
            new() { Name = "Jane Doe" }
        ];
        await _context.People.AddRangeAsync(people);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Person> results = await _repository.SearchAsync(null, null);

        // Assert
        Assert.That(results.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsync_RemovesPerson_WhenFound()
    {
        // Arrange
        Person person = new() { Name = "Test Person" };
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(person.Id);
        Person? result = await _context.People.FindAsync(person.Id);

        // Assert
        Assert.That(result, Is.Null);
    }
}
