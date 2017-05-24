using System.IO;

namespace LogAnalyzer
{
    public class LogAnalyzer
    {
        public int MinNameLength { get; set; }

        private ILogger service;
        public LogAnalyzer(ILogger service)
        {
            this.service = service;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                service.LogError("Слишком короткое имя файла " + fileName);
            }
        }

    }
}
