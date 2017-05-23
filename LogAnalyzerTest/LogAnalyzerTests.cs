using System;
using LogAnalyzer;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            FakeWebService stubService = new FakeWebService();
            stubService.ToThrow = new Exception("fake exception");
            FakeEmailService mockEmail = new FakeEmailService();
            LogAnalyzer2 log = new LogAnalyzer2(stubService, mockEmail);

            string tooShortFileName ="abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("someone@somewhere.com", mockEmail.To);
            StringAssert.Contains("fake exception", mockEmail.Body);
            StringAssert.Contains("can’t log", mockEmail.Subject);

        }
    }
}