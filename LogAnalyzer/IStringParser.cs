namespace LogAnalyzer
{
    public interface IStringParser
    {
        string StringToParse { get; }

        bool HasCorrectHeader();
        string GetStringVersionFromHeader();
    }
}
