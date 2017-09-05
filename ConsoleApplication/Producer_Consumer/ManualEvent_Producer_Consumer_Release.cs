using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication.Producer_Consumer
{
    class ProducerConsumerQueueManual : IDisposable
    {
        EventWaitHandle _wh = new ManualResetEvent(false);
        List<Thread> _workers = new List<Thread>();
        readonly object _locker = new object();
        Queue<string> _tasks = new Queue<string>();

        public ProducerConsumerQueueManual()
        {
            _workers.Add(new Thread(Work));
            _workers.Add(new Thread(Work));
            _workers.Add(new Thread(Work));

            _workers.ForEach(i => i.Start());
        }

        public void EnqueueTask(string task)
        {
            lock (_locker)
            {
                _tasks.Enqueue(task);
            }

            _wh.Set();
        }

        public void Dispose()
        {
            EnqueueTask(null); // Signal the consumer to exit.
            _workers.ForEach(i => i.Join()); // Wait for the consumer's thread to finish.
            _wh.Close(); // Release any OS resources.
        }

        void Work()
        {
            while (true)
            {
                string task = null;

                lock (_locker)
                {
                    if (_tasks.Count > 0)
                    {
                        task = _tasks.Dequeue();
                        if (task == null)
                        {
                            return;
                        }
                    }
                }

                if (task != null)
                {
                    Console.WriteLine("Performing task: " + task);
                    Thread.Sleep(1000); // simulate work...
                }
                else
                {
                    _wh.WaitOne(); // No more tasks - wait for a signal
                }
            }
        }
    }

    class ManualEvent_Producer_Consumer_Release
    {
        public static void MyMain()
        {
            using (ProducerConsumerQueueManual q = new ProducerConsumerQueueManual())
            {
                q.EnqueueTask("Hello");

                for (int i = 0; i < 10; i++)
                {
                    q.EnqueueTask("Say " + i);
                }

                q.EnqueueTask("Goodbye!");
            }
        }
    }
}
