using LogAnalyzer;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class ConfigurationManagerTests : BaseTestsClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            FakeTheLogger();
            var cm = new ConfigurationManager();
            var configured = cm.IsConfigured("something");
        }
    }
}