using System;
using System.Threading;

namespace ConsoleApplication.Producer_Consumer
{
    class ManualEventExample
    {
        public static Thread T1;
        public static Thread T2;
        public static Thread T3;
        //This ManualResetEvent starts out non-signalled
        public static ManualResetEvent mr1 = new ManualResetEvent(false);

        public static void MyMain()
        {
            T1 = new Thread((ThreadStart) delegate
            {
                Console.WriteLine("T1 is simulating some work by sleeping for 5 secs");
                //calling sleep to simulate some work
                Thread.Sleep(5000);
                Console.WriteLine("T1 is just about to set ManualResetEvent ar1");
                //alert waiting thread(s)
                mr1.Set();
            });

            T2 = new Thread((ThreadStart) delegate
            {
                //wait for ManualResetEvent mr1, this will wait for ar1
                //to be signalled from some other thread
                Console.WriteLine("T2 starting to wait for ManualResetEvent mr1, at time {0}", DateTime.Now.ToLongTimeString());
                mr1.WaitOne();
                Console.WriteLine("T2 finished waiting for ManualResetEvent mr1, at time {0}", DateTime.Now.ToLongTimeString());
            });

            T3 = new Thread((ThreadStart) delegate
            {
                //wait for ManualResetEvent mr1, this will wait for ar1
                //to be signalled from some other thread
                Console.WriteLine("T3 starting to wait for ManualResetEvent mr1, at time {0}", DateTime.Now.ToLongTimeString());
                mr1.WaitOne();
                Console.WriteLine("T3 finished waiting for ManualResetEvent mr1, at time {0}", DateTime.Now.ToLongTimeString());
            });

            T1.Name = "T1";
            T2.Name = "T2";
            T3.Name = "T3";

            T1.Start();
            T2.Start();
            T3.Start();

            Console.ReadLine();
        }
    }
}
