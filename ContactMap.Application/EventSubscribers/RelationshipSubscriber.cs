using ContactMap.Domain.Events;
using Yaref92.Events.Abstractions;

namespace ContactMap.Application.EventSubscribers;

/// <summary>
/// Handles relationship-related domain events asynchronously.
/// </summary>
public class RelationshipSubscriber :
    IAsyncEventSubscriber<RelationshipRequested>,
    IAsyncEventSubscriber<RelationshipApproved>
{
    /// <summary>
    /// Handles the RelationshipRequested asynchronously.
    /// </summary>
    public async Task OnNextAsync(RelationshipRequested @event, CancellationToken cancellationToken = default)
    {
        // TODO: Add logic for when a relationship is requested (e.g., send notification)
        throw new NotImplementedException();
    }

    /// <summary>
    /// Handles the RelationshipApproved asynchronously.
    /// </summary>
    public async Task OnNextAsync(RelationshipApproved @event, CancellationToken cancellationToken = default)
    {
        // TODO: Add logic for when a relationship is approved (e.g., notify both users)
        throw new NotImplementedException();
    }
}
