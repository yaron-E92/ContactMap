using ContactMap.Domain.Entities;
using ContactMap.Domain.Repositories;

namespace ContactMap.Application.UseCases;

/// <summary>
/// Defines the contract for a use case that searches for people.
/// </summary>
public interface ISearchPeopleUseCase
{
    /// <summary>
    /// Searches for people by name and/or community.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="community">The community to search for.</param>
    /// <returns>A collection of matching people.</returns>
    Task<IEnumerable<Person>> SearchPeopleAsync(string? name, string? community);
}

/// <summary>
/// Implements the use case for searching people.
/// </summary>
public class SearchPeopleUseCase : ISearchPeopleUseCase
{
    private readonly IPersonRepository _personRepository;
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchPeopleUseCase"/> class.
    /// </summary>
    /// <param name="personRepository">The person repository to use.</param>
    public SearchPeopleUseCase(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    /// <inheritdoc/>
    public async Task<IEnumerable<Person>> SearchPeopleAsync(string? name, string? community)
    {
        return await _personRepository.SearchAsync(name, community);
    }
}
