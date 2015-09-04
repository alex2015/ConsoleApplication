using System;


namespace ConsoleApplication
{
    public class LogEntryEventArgs : EventArgs
    {
        public string LogEntry { get; internal set; }
    }

    public class LogFileReader
    {
        private readonly string _logFileName;
        public LogFileReader(string logFileName)
        {
            _logFileName = logFileName;
        }

        public event EventHandler<LogEntryEventArgs> OnNewLogEntry;

        private void RaiseNewLogEntry(string logEntry)
        {
            var handler = OnNewLogEntry;
            if (handler != null)
            {
                handler(this, new LogEntryEventArgs());
            }
        }
    }
}
