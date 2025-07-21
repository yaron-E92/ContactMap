using ContactMap.Application.Interfaces;
using ContactMap.Domain.Entities;
using ContactMap.Domain.Events;
using ContactMap.Domain.Repositories;
using Yaref92.Events.Abstractions;

namespace ContactMap.Application.Commands;

public class ApproveRelationshipHandler(IRelationshipRepository relationshipRepo,
                                               IEventAggregator eventAggregator) : ICommandHandler<ApproveRelationship>
{
    private readonly IRelationshipRepository _relationshipRepo = relationshipRepo;
    private readonly IEventAggregator _eventAggregator = eventAggregator;

    public async Task Handle(ApproveRelationship command, CancellationToken cancellationToken = default)
    {
        Relationship relationship = await _relationshipRepo.GetByIdAsync(command.RelationshipId)
                                    ?? throw new InvalidOperationException("Relationship not found");
        relationship.Status = RelationshipStatus.Approved;
        relationship.RespondedAt = DateTime.UtcNow;
        bool updated = await _relationshipRepo.UpdateAsync(relationship);
        if (!updated)
        {
            // For now, throw; in the future, log or handle gracefully
            throw new InvalidOperationException("Relationship not found during update");
        }
        var evt = new RelationshipApproved(relationship.Id, relationship.RequesterId, relationship.AddresseeId);
        await _eventAggregator.PublishEventAsync(evt, cancellationToken);
    }
}
