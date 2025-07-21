using ContactMap.Domain.Entities;

namespace ContactMap.Domain.Tests;

[TestFixture]
public class CommunityTests
{
    [Test]
    public void Can_Construct_Community_With_Properties()
    {
        const string name = "Test Community";
        var community = new Community
        {
            Name = name,
            Members = []
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(community.Name, Is.EqualTo(name));
            Assert.That(community.Members, Is.Not.Null);
        }
    }
}
