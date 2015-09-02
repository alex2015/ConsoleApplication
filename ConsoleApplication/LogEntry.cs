namespace ConsoleApplication
{
    public class LogEntry : LogEntryBase
    {
        public static LogEntry Parse(string line)
        {
            return new LogEntry();
        }
    }
}
