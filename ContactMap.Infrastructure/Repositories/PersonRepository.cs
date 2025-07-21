using ContactMap.Domain.Entities;
using ContactMap.Domain.Repositories;
using ContactMap.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactMap.Infrastructure.Repositories;

/// <summary>
/// Provides an implementation of <see cref="IPersonRepository"/> using Entity Framework Core.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PersonRepository"/> class.
/// </remarks>
/// <param name="db">The database context to use.</param>
public class PersonRepository(ContactMapDbContext db) : IPersonRepository
{
    private readonly ContactMapDbContext _db = db;

    /// <inheritdoc/>
    public async Task<Person?> GetByIdAsync(Guid id) =>
        await _db.People.Include(p => p.Communities).Include(p => p.Events).FirstOrDefaultAsync(p => p.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<Person>> SearchAsync(string? name, string? community)
    {
        IQueryable<Person> query = _db.People.Include(p => p.Communities).AsQueryable();
        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(p => p.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(community))
            query = query.Where(p => p.Communities.Any(c => c.Name.Contains(community)));
        return await query.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task AddAsync(Person person)
    {
        _db.People.Add(person);
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Person person)
    {
        bool exists = await _db.People.AnyAsync(p => p.Id == person.Id);
        if (!exists)
            return false;
        _db.People.Update(person);
        await _db.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id)
    {
        Person? person = await _db.People.FindAsync(id);
        if (person is not null)
        {
            _db.People.Remove(person);
            await _db.SaveChangesAsync();
        }
    }
}
