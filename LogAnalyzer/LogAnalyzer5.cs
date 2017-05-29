using System;

namespace LogAnalyzer
{
    public class LogAnalyzer5
    {
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                LoggingFacility.Log("Слишком короткое имя файла" +fileName);
            }
        }
    }

    public class ConfigurationManager
    {
        public bool IsConfigured(string configName)
        {
            LoggingFacility.Log("проверяется" +configName);
            return true;
        }
    }
}
