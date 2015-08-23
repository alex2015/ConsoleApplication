using System.Text;

namespace ConsoleApplication
{
    public static class LogEntryBaseEx
    {
        public static string GetText(this LogEntryBase logEntry)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("[{0}]", logEntry.EntryDateTime)
                .AppendFormat("[{0}]", logEntry.Severity)
                .AppendFormat("[{0}]", logEntry.Message)
                .AppendFormat("[{0}]", logEntry.AdditionalInformation);

            return sb.ToString();
        }
    }
}
