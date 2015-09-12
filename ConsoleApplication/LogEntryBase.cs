using System;
using ConsoleApplication.Visitor;

namespace ConsoleApplication
{
    public abstract class LogEntryBase
    {
        public DateTime EntryDateTime { get; internal set; }
        public Severity Severity { get; internal set; }
        public String Message { get; internal set; }
        public String AdditionalInformation { get; internal set; }

        public abstract void Accept(ILogEntryVisitor logEntryVisitor);
    }
}
