using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.AsyncProgram
{
    class MyAsyncProgram
    {
        public static void MyMain()
        {
            //DumpWebPage("https://www.microsoft.com/ru-ru/");
            DumpWebPageAsync("https://www.microsoft.com/ru-ru/");
            Console.ReadKey();
        }

        private static void DumpWebPage(string uri)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(uri);
            Console.WriteLine(page);

            Console.ReadKey();
        }

        private static async void DumpWebPageAsync(string uri)
        {
            WebClient webClient = new WebClient();
            string page = await webClient.DownloadStringTaskAsync(uri);
            Console.WriteLine(page);
        }
    }
}
