namespace FINS.UnitTest.TestFixtures
{
    public class OrgUserRoleFixture : InMemoryContextTest
    {
        public OrgUserRoleFixture()
        {
            SampleDataGenerator.InsertOrgUserRoleTestData().Wait();
        }
    }
}
