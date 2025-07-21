using NUnit.Framework;
using System.Net.Http.Json;
using ContactMap.Domain.Entities;
using ContactMap.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactMap.IntegrationTests;

[TestFixture]
[Explicit("Integration tests for the PeopleController")]
[Category("Integration")]
public class PeopleControllerTests : IDisposable
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;
    private IServiceScope _scope = null!;
    private ContactMapDbContext _db = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the real db context
                    ServiceDescriptor? descriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ContactMapDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Add in-memory database
                    services.AddDbContext<ContactMapDbContext>(options =>
                        options.UseInMemoryDatabase("IntegrationTestDb"));
                });
            });

        _client = _factory.CreateClient();
    }

    [SetUp]
    public void Setup()
    {
        _scope = _factory.Services.CreateScope();
        _db = _scope.ServiceProvider.GetRequiredService<ContactMapDbContext>();
    }

    [TearDown]
    public void TearDown()
    {
        _db.Database.EnsureDeleted();
        _scope.Dispose();
    }

    public void Dispose()
    {
        _factory?.Dispose();
        _client?.Dispose();
    }

    [Test]
    public async Task SearchPeople_ReturnsMatchingPeople()
    {
        // Arrange
        var person = new Person
        {
            Name = "John Doe",
            Address = new Domain.ValueObjects.Address
            {
                Street = "123 Main St",
                City = "Test City",
                State = "TS",
                Country = "Test Country",
                PostalCode = "12345"
            }
        };
        await _db.People.AddAsync(person);
        await _db.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _client.GetAsync("/api/people/search?name=John");

        // Assert
        using (Assert.EnterMultipleScope())
        {
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Status: {response.StatusCode}\nBody: {error}");
            }
            else
            {
                IEnumerable<Person>? people = await response.Content.ReadFromJsonAsync<IEnumerable<Person>>();
                Assert.That(people, Is.Not.Null);
                var personList = people!.ToList();
                Assert.That(personList, Has.Count.EqualTo(1));
                Assert.That(personList[0].Name, Is.EqualTo("John Doe"));
            }
        }
    }

    [Test]
    public async Task SearchPeople_ReturnsEmptyList_WhenNoMatches()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/api/people/search?name=NonExistent");

        // Assert
        using (Assert.EnterMultipleScope())
        {
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Status: {response.StatusCode}\nBody: {error}");
            }
            else
            {
                IEnumerable<Person>? people = await response.Content.ReadFromJsonAsync<IEnumerable<Person>>();
                Assert.That(people, Is.Not.Null);
                Assert.That(people, Is.Empty);
            }
        }
    }
}
