using System.IO;

namespace LogAnalyzer
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName);
        }
        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }


        private IWebService service;
        public LogAnalyzer(IWebService service)
        {
            this.service = service;
        }
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                service.LogError("Слишком короткое имя файла " + fileName);
            }
        }
    }
}
