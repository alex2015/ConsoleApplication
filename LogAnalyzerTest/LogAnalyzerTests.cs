using System;
using LogAnalyzer;
using NSubstitute;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            var logger = new FakeLogger();
            var analyzer = new LogAnalyzer.LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");
            StringAssert.Contains("Слишком короткое", logger.LastError);
        }

        [Test]
        public void Analyze_TooShortFileName_CallLogger1()
        {
            var logger = Substitute.For<ILogger>();
            var analyzer = new LogAnalyzer.LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");
            logger.Received().LogError("Слишком короткое имя файла a.txt");
        }

        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            var fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.IsValidLogFileName(Arg.Any<String>()).Returns(true);
            Assert.IsTrue(fakeRules.IsValidLogFileName("anything.txt"));
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            var fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.When(x => x.IsValidLogFileName(Arg.Any<string>()))
            .Do(context => { throw new Exception("fake exception"); });
            Assert.Throws<Exception>(() => fakeRules.IsValidLogFileName("anything"));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockWebService = Substitute.For<IWebService3>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(
            logger => logger.LogError(Arg.Any<string>()))
            .Do(info => { throw new Exception("fake exception"); });
            var analyzer = new LogAnalyzer3(stubLogger, mockWebService );
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockWebService = Substitute.For<IWebService4>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
            .Do(info => { throw new Exception("fake exception"); });
            var analyzer = new LogAnalyzer4(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000 && info.Message.Contains("fake exception")));
        }

    }

    class FakeLogger : ILogger
    {
        public string LastError;
        public void LogError(string message)
        {
            LastError = message;
        }
    }
}