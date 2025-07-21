using Yaref92.Events;

namespace ContactMap.Domain.Events;

public class RelationshipApproved(Guid relationshipId, Guid requesterId, Guid addresseeId) : DomainEventBase()
{
    public Guid RelationshipId { get; } = relationshipId;
    public Guid RequesterId { get; } = requesterId;
    public Guid AddresseeId { get; } = addresseeId;
}
