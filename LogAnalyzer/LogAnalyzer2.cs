using System;

namespace LogAnalyzer
{
    public class LogAnalyzer2
    {
        public LogAnalyzer2(IWebService service, IEmailService email)
        {
            Email = email;
            Service = service;
        }

        public IWebService Service { get; set; }
        public IEmailService Email { get; set; }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    Service.LogError("слишком короткое имя файла " + fileName);
                }
                catch (Exception e)
                {
                    Email.SendEmail("someone@somewhere.com", "can’t log", e.Message);
                }
            }
        }
    }
}
