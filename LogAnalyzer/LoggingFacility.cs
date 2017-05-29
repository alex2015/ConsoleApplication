using System;

namespace LogAnalyzer
{
    public class LoggingFacility
    {
        public static void Log(string text)
        {
            logger.Log(text);
        }

        private static ILogger5 logger;

        public static ILogger5 Logger
        {
            get { return logger; }
            set { logger = value; }
        }
    }
}
