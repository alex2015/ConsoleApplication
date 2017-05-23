namespace LogAnalyzer
{
    public class FakeWebService : IWebService
    {
        public string LastError;
        public void LogError(string message)
        {
            LastError = message;
        }
    }
}
