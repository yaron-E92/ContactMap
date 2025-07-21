using ContactMap.Domain.Entities;

namespace ContactMap.Domain.Repositories;

public interface IRelationshipRepository
{
    Task<Relationship?> GetByIdAsync(Guid id);
    Task<IEnumerable<Relationship>> GetByPersonIdAsync(Guid personId);
    Task AddAsync(Relationship relationship);
    /// <summary>
    /// Updates an existing relationship in the repository.
    /// </summary>
    /// <param name="relationship">The relationship to update.</param>
    /// <returns>True if the relationship was updated; false if it does not exist.</returns>
    Task<bool> UpdateAsync(Relationship relationship);
    Task DeleteAsync(Guid id);
}
