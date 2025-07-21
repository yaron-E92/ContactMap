using ContactMap.Application.Interfaces;

namespace ContactMap.Application.Commands;

public class ApproveRelationship(Guid relationshipId) : ICommand
{
    public Guid RelationshipId { get; } = relationshipId;
}
