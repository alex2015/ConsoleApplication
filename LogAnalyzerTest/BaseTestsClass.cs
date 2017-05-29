using LogAnalyzer;
using NSubstitute;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class BaseTestsClass
    {
        public ILogger5 FakeTheLogger()
        {
            LoggingFacility.Logger = Substitute.For<ILogger5>();
            return LoggingFacility.Logger;
        }

        [TearDown]
        public void teardown()
        {
            LoggingFacility.Logger = null;
        }
    }
}