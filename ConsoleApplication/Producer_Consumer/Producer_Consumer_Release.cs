using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ConsoleApplication.Producer_Consumer
{
    class Producer_Consumer_Release
    {
        BlockingCollection<string> q = new BlockingCollection<string>();

        public static void MyMain()
        {
            new Producer_Consumer_Release().Main();
        }

        public void Main()
        {
            var threads = new[] { new Thread(Consumer), new Thread(Consumer) };
            foreach (var t in threads)
            {
                t.Start();
            }

            string s;
            while ((s = Console.ReadLine()).Length != 0)
            {
                q.Add(s);
            }

            q.CompleteAdding(); // останавливаем

            foreach (var t in threads)
            {
                t.Join();
            }
        }

        void Consumer()
        {
            foreach (var s in q.GetConsumingEnumerable())
            {
                Console.WriteLine("Processing: {0}", s);
                Thread.Sleep(10000);
                Console.WriteLine("Processed: {0}", s);
            }
        }
    }
}
