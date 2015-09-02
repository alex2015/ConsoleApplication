using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class LogFileSource : IEnumerable<LogEntry>
    {
        private readonly string _logFileName;
        public LogFileSource(string logFileName)
        {
            _logFileName = logFileName;
        }

        public IEnumerator<LogEntry> GetEnumerator()
        {
            foreach (var line in File.ReadAllLines(_logFileName))
            {
                yield return LogEntry.Parse(line);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static void ForEachIEnumerable(IEnumerable sequence)
        {
            // foreach(var e in sequence) {Console.WriteLine(e);}
            IEnumerator enumerator = sequence.GetEnumerator();
            object current = null;
            try
            {
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Console.WriteLine(current);
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
