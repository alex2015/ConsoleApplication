using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class LogAnalyzerTests5 : BaseTestsClass
    {
        [Test]
        public void Analyze_EmptyFile_ThrowsException()
        {
            FakeTheLogger();
            var la = new LogAnalyzer.LogAnalyzer5();
            la.Analyze("myemptyfile.txt");

            // остальная часть теста
        }
    }
}