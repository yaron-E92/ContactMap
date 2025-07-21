using ContactMap.Domain.Repositories;
using ContactMap.Infrastructure.Persistence;
using ContactMap.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactMap.Infrastructure;

public static class InfrastructureDI
{
    /// <summary>
    /// Registers the ContactMap persistence layer (DbContext and repositories) with the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="useInMemory">If true, uses InMemory provider; otherwise, expects further configuration.</param>
    /// <param name="dbName">The name of the in-memory database (if used).</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddInfrastructureDI(
        this IServiceCollection services,
        bool useInMemory = true,
        string dbName = "ContactMapDb")
    {
        if (useInMemory)
        {
            services.AddDbContext<ContactMapDbContext>(options =>
                options.UseInMemoryDatabase("ContactMapInMemoryDb"));
        }
        else
        {
            services.AddDbContext<ContactMapDbContext>(options =>
                options.UseSqlite(dbName)); // Possibly change to PostgreSQL or other providers as needed
        }

        // Register repositories
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IRelationshipRepository, RelationshipRepository>();

        return services;
    }
}
