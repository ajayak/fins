using Xunit;

namespace FINS.UnitTest.TestFixtures
{
    [CollectionDefinition("OrgUserRole")]
    public class OrgUserRoleCollection : ICollectionFixture<OrgUserRoleFixture>
    {
        
    }

    public class OrgUserRoleFixture : InMemoryContextTest
    {
        public OrgUserRoleFixture()
        {
            SampleDataGenerator.InsertOrgUserRoleTestData().Wait();
        }
    }
}
