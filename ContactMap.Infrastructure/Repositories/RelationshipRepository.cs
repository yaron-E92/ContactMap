using ContactMap.Domain.Entities;
using ContactMap.Domain.Repositories;
using ContactMap.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactMap.Infrastructure.Repositories;

public class RelationshipRepository(ContactMapDbContext db) : IRelationshipRepository
{
    private readonly ContactMapDbContext _db = db;

    public async Task<Relationship?> GetByIdAsync(Guid id) =>
        await _db.Relationships.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Relationship>> GetByPersonIdAsync(Guid personId) =>
        await _db.Relationships.Where(r => r.RequesterId == personId || r.AddresseeId == personId).ToListAsync();

    public async Task AddAsync(Relationship relationship)
    {
        _db.Relationships.Add(relationship);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Relationship relationship)
    {
        bool exists = await _db.Relationships.AnyAsync(r => r.Id == relationship.Id);
        if (!exists)
            return false;
        _db.Relationships.Update(relationship);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Guid id)
    {
        Relationship? relationship = await _db.Relationships.FindAsync(id);
        if (relationship is not null)
        {
            _db.Relationships.Remove(relationship);
            await _db.SaveChangesAsync();
        }
    }
}
