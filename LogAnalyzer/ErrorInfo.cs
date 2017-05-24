using System;

namespace LogAnalyzer
{
    public class ErrorInfo
    {
        public int Severity { get; set; }
        public string Message { get; set; }

        public ErrorInfo()
        {
        }

        public ErrorInfo(int s, string m)
        {
            Severity = s;
            Message = m;
        }
    }
}
