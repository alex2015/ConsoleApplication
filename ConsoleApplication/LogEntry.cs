using ConsoleApplication.Visitor;

namespace ConsoleApplication
{
    public class LogEntry : LogEntryBase
    {
        public static LogEntry Parse(string line)
        {
            return new LogEntry();
        }

        public override void Accept(ILogEntryVisitor logEntryVisitor)
        {
            logEntryVisitor.Visit(this);
        }
    }
}
