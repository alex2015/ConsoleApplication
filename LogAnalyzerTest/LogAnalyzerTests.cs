using System;
using LogAnalyzer;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void overrideTest()
        {
            FakeExtensionManager stub = new FakeExtensionManager();
            stub.WillBeValid = true;
            TestableLogAnalyzer logan = new TestableLogAnalyzer(stub);

            bool result = logan.IsValidLogFileName("file.ext");
            Assert.True(result);
        }
    }

    class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid { get; set; }

        public bool IsValid(string fileName)
        {
            return true;
        }
    }

    class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public TestableLogAnalyzer(IExtensionManager mgr)
        {
            Manager = mgr;
        }
        public IExtensionManager Manager;

        protected override IExtensionManager GetManager()
        {
            return Manager;
        }
    }
}



