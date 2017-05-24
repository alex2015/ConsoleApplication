namespace LogAnalyzer
{
    public interface IWebService
    {
        void LogError(string message);
    }

    public interface IWebService3
    {
        void Write(string message);
    }

    public interface IWebService4
    {
        void Write(ErrorInfo info);
    }
}
