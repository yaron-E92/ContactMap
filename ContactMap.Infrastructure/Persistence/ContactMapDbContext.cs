using Microsoft.EntityFrameworkCore;
using ContactMap.Domain.Entities;

namespace ContactMap.Infrastructure.Persistence;

/// <summary>
/// Represents the Entity Framework Core database context for the ContactMap application.
/// </summary>
public class ContactMapDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactMapDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ContactMapDbContext(DbContextOptions<ContactMapDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the people in the database.
    /// </summary>
    public DbSet<Person> People { get; set; }
    /// <summary>
    /// Gets or sets the communities in the database.
    /// </summary>
    public DbSet<Community> Communities { get; set; }
    /// <summary>
    /// Gets or sets the events in the database.
    /// </summary>
    public DbSet<Event> Events { get; set; }
    /// <summary>
    /// Gets or sets the relationships in the database.
    /// </summary>
    public DbSet<Relationship> Relationships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Address as an owned type
        modelBuilder.Entity<Person>(entity =>
        {
            entity.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.State).HasMaxLength(100);
                address.Property(a => a.PostalCode).HasMaxLength(20);
                address.Property(a => a.Country).HasMaxLength(100);
            });
        });

        // Configure ContactDetails as an owned type
        modelBuilder.Entity<Person>(entity =>
        {
            entity.OwnsOne(p => p.ContactDetails, contactDetails =>
            {
                contactDetails.Property(c => c.Phone).HasMaxLength(15);
                contactDetails.Property(c => c.Email).HasMaxLength(200);
                contactDetails.Property(c => c.Social).HasMaxLength(100);
            });
        });
    }
}
