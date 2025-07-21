using ContactMap.Application.UseCases;
using ContactMap.Domain.Repositories;
using ContactMap.Domain.Entities;
using NSubstitute;

namespace ContactMap.Application.Tests.UseCases;

[TestFixture]
public class SearchPeopleUseCaseTests
{
    private IPersonRepository _repository = null!;
    private SearchPeopleUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IPersonRepository>();
        _useCase = new SearchPeopleUseCase(_repository);
    }

    [Test]
    public async Task SearchPeopleAsync_CallsRepository_WithSameParameters()
    {
        // Arrange
        const string name = "John";
        const string community = "Dev";
        Person[] expectedPeople = [new Person { Name = name }];
        _repository.SearchAsync(name, community).Returns(expectedPeople);

        // Act
        IEnumerable<Person> result = await _useCase.SearchPeopleAsync(name, community);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            await _repository.Received(1).SearchAsync(name, community);
            Assert.That(result, Is.EqualTo(expectedPeople));
        }
    }

    [Test]
    public async Task SearchPeopleAsync_WithNullParameters_CallsRepository()
    {
        // Arrange
        Person[] expectedPeople = [];
        _repository.SearchAsync(null, null).Returns(expectedPeople);

        // Act
        IEnumerable<Person> result = await _useCase.SearchPeopleAsync(null, null);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            await _repository.Received(1).SearchAsync(null, null);
            Assert.That(result, Is.EqualTo(expectedPeople));
        }
    }
}
