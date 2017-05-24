using System;

namespace LogAnalyzer
{
    public class LogAnalyzer3
    {
        private ILogger _logger;
        private IWebService3 _webService;

        public LogAnalyzer3(ILogger logger, IWebService3 webService)
        {
            _logger = logger;
            _webService = webService;
        }

        public int MinNameLength { get; set; }
        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
            {
                try
                {
                    _logger.LogError(
                    string.Format("Слишком короткое имя файла: {0}",filename ));
                }
                catch (Exception e)
                {
                    _webService.Write("Ошибка регистратора: " +e);
                }
            }
        }
    }
}
