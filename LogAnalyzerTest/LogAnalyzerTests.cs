using LogAnalyzer;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            FakeWebService mockService = new FakeWebService();
            LogAnalyzer.LogAnalyzer log = new LogAnalyzer.LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("Слишком короткое имя файла ", mockService.LastError);
        }
    }
}