using Xunit;

namespace FINS.UnitTest.TestFixtures
{
    [CollectionDefinition("TestData")]
    public class TestDataCollection : ICollectionFixture<TestDataFixture>
    {
        
    }

    public class TestDataFixture : InMemoryContextTest
    {
        public TestDataFixture()
        {
            SampleDataGenerator.InsertDemoData().Wait();
        }
    }
}
