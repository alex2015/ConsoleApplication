using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    class MyAsyncException
    {
        public static void MyMain()
        {
            MyMain6();
            Console.WriteLine("main");
            Console.ReadKey();
        }

        #region MyMain1

        public static async void MyMain1()
        {
            var t = Catcher();

            t.GetAwaiter().GetResult();

            Console.ReadKey();
        }

        async static Task Catcher()
        {
            try
            {
                var t = Thrower();

                await t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                // Исключение будет обработано здесь
            }
        }

        async static Task Thrower()
        {
            throw new Exception();
            await Task.Delay(100);
            throw new Exception();
        }

        #endregion


        #region MyMain2

        private static async void MyMain2()
        {
            Task<string> task = ReadFileAsync(@"D:\test_file1.txt");

            try
            {
                string text = await task;
                Console.WriteLine("File contents: {0}", text);
            }
            catch (IOException e)
            {
                Console.WriteLine("Caught IOException:{0}", e.Message);
            }
        }

        private static async Task<string> ReadFileAsync(string filename)
        {
            using (var reader = File.OpenText(filename))
            {
                return await reader.ReadToEndAsync();
            }
        }

        #endregion

        #region MyMain3

        private static void MyMain3()
        {
            Func<int, Task<int>> function = async x =>
            {
                Console.WriteLine("Starting... x={0}", x);
                await Task.Delay(x*1000);
                Console.WriteLine("Finished... x={0}", x);
                return x*2;
            };

            Task<int> first = function(5);
            Task<int> second = function(3);
            Console.WriteLine("First result: {0}", first.Result);
            Console.WriteLine("Second result: {0}", second.Result);
        }

        #endregion

        #region MyMain4

        private static void MyMain4()
        {
            ComputeLengthAsync("asdasdasdasd");
            Console.ReadKey();
        }

        private static Task<int> ComputeLengthAsync(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            Func<Task<int>> func = async () =>
            {
                await Task.Delay(500);
                return text.Length;
            };

            return func();
        }

        #endregion

        #region MyMain5

        private static void MyMain5()
        {
            var r = SumCharactersAsync("qwewqeqwe").Result;

            Console.ReadKey();
        }

        private static async Task<int> SumCharactersAsync(IEnumerable<char> text)
        {
            int total = 0;
            foreach (char ch in text)
            {
                int unicode = ch;
                await Task.Delay(unicode);
                total += unicode;
            }
            await Task.Yield();
            return total;
        }

        #endregion

        #region MyMain6

        private static readonly List<string> s_Domains = new List<string>
        {
            "google.com",
            "bing.com",
            "oreilly.com",
            "simple-talk.com",
            //"microsoft.com",
            "facebook.com",
            "twitter.com",
            "reddit.com",
            "baidu.com",
            "bbc.co.uk"
        };

        private static async void MyMain6()
        {
            var q = await GetSiteString(s_Domains.Select(i => "http://" + i).ToList());

            Console.WriteLine(q);
        }

        private static async Task<int> GetSiteString(IEnumerable<string> urls)
        {
            var summa = 0;
            foreach (string url in urls)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var str = await client.GetStringAsync(url);
                        summa += str.Length;
                    }
                }
                catch (WebException exception)
                {
                    // ЧТО ДЕЛАТЬ: занести в журнал, обновить статистику и т.д.
                }
            }

            return summa;
        }

        #endregion
    }
}
