using System;

namespace ConsoleApplication.Visitor
{
    public class DatabaseLogSaver : ILogEntryVisitor
    {
        public void SaveLogEntry(LogEntryBase logEntry)
        {
            logEntry.Accept(this);
        }

        void ILogEntryVisitor.Visit(ExceptionLogEntry exceptionLogEntry)
        {
            SaveException(exceptionLogEntry);
        }
        void ILogEntryVisitor.Visit(LogEntry simpleLogEntry)
        {
            SaveSimpleLogEntry(simpleLogEntry);
        }

        private void SaveSimpleLogEntry(LogEntry logEntry)
        {
        }

        private void SaveException(ExceptionLogEntry exceptionLogEntry)
        {
        }
    }
}
