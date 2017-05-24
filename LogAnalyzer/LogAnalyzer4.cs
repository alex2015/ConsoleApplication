using System;

namespace LogAnalyzer
{
    public class LogAnalyzer4
    {
        private ILogger _logger;
        private IWebService4 _webService;

        public LogAnalyzer4(ILogger logger, IWebService4 webService)
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
                    _webService.Write(new ErrorInfo
                    {
                        Severity = 1000,
                        Message = "fake exception"
                    });
                }
            }
        }
    }
}
