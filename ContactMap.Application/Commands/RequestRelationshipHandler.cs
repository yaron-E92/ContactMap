using ContactMap.Application.Interfaces;
using ContactMap.Domain.Entities;
using ContactMap.Domain.Events;
using ContactMap.Domain.Repositories;
using Yaref92.Events.Abstractions;

namespace ContactMap.Application.Commands;

public class RequestRelationshipHandler(IRelationshipRepository relationshipRepo,
                                               IEventAggregator eventAggregator) : ICommandHandler<RequestRelationship>
{
    private readonly IRelationshipRepository _relationshipRepo = relationshipRepo;
    private readonly IEventAggregator _eventAggregator = eventAggregator;

    public async Task Handle(RequestRelationship command, CancellationToken cancellationToken = default)
    {
        var relationship = new Relationship
        {
            RequesterId = command.RequesterId,
            AddresseeId = command.AddresseeId,
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        await _relationshipRepo.AddAsync(relationship);
        var evt = new RelationshipRequested(relationship.Id, command.RequesterId, command.AddresseeId);
        await _eventAggregator.PublishEventAsync(evt, cancellationToken);
    }
}
