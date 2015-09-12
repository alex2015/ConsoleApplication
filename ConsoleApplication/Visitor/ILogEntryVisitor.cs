namespace ConsoleApplication.Visitor
{
    public interface ILogEntryVisitor
    {
        void Visit(ExceptionLogEntry exceptionLogEntry);
        void Visit(LogEntry simpleLogEntry);
    }
}
