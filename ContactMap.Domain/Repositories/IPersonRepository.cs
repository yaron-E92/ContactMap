using ContactMap.Domain.Entities;

namespace ContactMap.Domain.Repositories;

/// <summary>
/// Defines the contract for a repository that manages Person entities.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>The person if found; otherwise, null.</returns>
    Task<Person?> GetByIdAsync(Guid id);
    /// <summary>
    /// Searches for people by name and/or community.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="community">The community to search for.</param>
    /// <returns>A collection of matching people.</returns>
    Task<IEnumerable<Person>> SearchAsync(string? name, string? community);
    /// <summary>
    /// Adds a new person to the repository.
    /// </summary>
    /// <param name="person">The person to add.</param>
    Task AddAsync(Person person);
    /// <summary>
    /// Updates an existing person in the repository.
    /// </summary>
    /// <param name="person">The person to update.</param>
    /// <returns>True if the person was updated; false if the person does not exist.</returns>
    Task<bool> UpdateAsync(Person person);
    /// <summary>
    /// Deletes a person from the repository by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person to delete.</param>
    Task DeleteAsync(Guid id);
}
