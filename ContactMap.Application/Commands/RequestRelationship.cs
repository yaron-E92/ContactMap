using ContactMap.Application.Interfaces;

namespace ContactMap.Application.Commands;

public class RequestRelationship(Guid requesterId, Guid addresseeId) : ICommand
{
    public Guid RequesterId { get; } = requesterId;
    public Guid AddresseeId { get; } = addresseeId;
}
