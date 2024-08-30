using Xunit;

namespace Accounting.TestTools.Configurations
{
    [CollectionDefinition("SequentialTests", DisableParallelization = true)]
    public class SequentialTestsCollection : ICollectionFixture<TestConfig>
    {
        
    }
}
