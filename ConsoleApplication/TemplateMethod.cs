using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public interface ILogSaver
    {
        void UploadLogEntries(IEnumerable<LogEntry> logEntries);
        void UploadExceptions(IEnumerable<ExceptionLogEntry> exceptions);
    }

    public class ClientBase<T>
    {
    }

    class LogSaverProxy : ILogSaver
    {
        class LogSaverClient : ClientBase<ILogSaver>
        {
            public ILogSaver LogSaver
            {
                get { return null; }
            }
        }


        public void UploadLogEntries(IEnumerable<LogEntry> logEntries)
        {
            UseProxyClient(c => c.UploadLogEntries(logEntries));
        }

        public void UploadExceptions(IEnumerable<ExceptionLogEntry> exceptions)
        {
            UseProxyClient(c => c.UploadExceptions(exceptions));
        }

        private void UseProxyClient(Action<ILogSaver> accessor)
        {
            var client = new LogSaverClient();

            try
            {
                accessor(client.LogSaver);
            }
            catch (Exception e)
            {
                
            }
        }
    }

}
