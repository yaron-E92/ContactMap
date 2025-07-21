using ContactMap.Domain.Entities;

namespace ContactMap.Domain.Tests;

[TestFixture]
public class RelationshipTests
{
    [Test]
    public void Can_Construct_Relationship_With_Properties()
    {
        var rel = new Relationship
        {
            RequesterId = Guid.NewGuid(),
            AddresseeId = Guid.NewGuid(),
            Status = RelationshipStatus.Pending,
            RequestedAt = DateTime.UtcNow,
            RespondedAt = null
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(rel.Status, Is.EqualTo(RelationshipStatus.Pending));
            Assert.That(rel.RespondedAt, Is.Null);
        }
    }
}
