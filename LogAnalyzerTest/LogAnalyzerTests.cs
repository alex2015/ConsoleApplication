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
            FakeLogger logger = new FakeLogger();
            LogAnalyzer.LogAnalyzer analyzer = new LogAnalyzer.LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");
            StringAssert.Contains("Слишком короткое", logger.LastError);
        }

        [Test]
        public void Analyze_TooShortFileName_CallLogger1()
        {
            ILogger logger = Substitute.For<ILogger>();
            LogAnalyzer.LogAnalyzer analyzer = new LogAnalyzer.LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");
            logger.Received().LogError("Слишком короткое имя файла a.txt");
        }

        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.IsValidLogFileName(Arg.Any<String>()).Returns(true);
            Assert.IsTrue(fakeRules.IsValidLogFileName("anything.txt"));
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.When(x => x.IsValidLogFileName(Arg.Any<string>()))
            .Do(context => { throw new Exception("fake exception"); });
            Assert.Throws<Exception>(() => fakeRules.IsValidLogFileName("anything"));
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