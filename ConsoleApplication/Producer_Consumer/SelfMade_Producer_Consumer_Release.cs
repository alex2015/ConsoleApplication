using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication.Producer_Consumer
{
    public class ProducerConsumer<T> where T : class
    {
        object mutex = new object();
        Queue<T> queue = new Queue<T>();
        bool isDead = false;

        public void Enqueue(T task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }

            lock (mutex)
            {
                if (isDead)
                {
                    throw new InvalidOperationException("Queue already stopped");
                }

                queue.Enqueue(task);
                Monitor.Pulse(mutex);
            }
        }

        public T Dequeue()
        {
            lock (mutex)
            {
                while (queue.Count == 0 && !isDead)
                {
                    Monitor.Wait(mutex);
                }

                if (queue.Count == 0)
                {
                    return null;
                }

                return queue.Dequeue();
            }
        }

        public void Stop()
        {
            lock (mutex)
            {
                isDead = true;
                Monitor.PulseAll(mutex);
            }
        }
    }

    class SelfMade_Producer_Consumer_Release
    {
        ProducerConsumer<string> q = new ProducerConsumer<string>();

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
                q.Enqueue(s);
            }

            q.Stop();

            foreach (var t in threads)
            {
                t.Join();
            }
        }

        void Consumer()
        {
            while (true)
            {
                string s = q.Dequeue();

                if (s == null)
                {
                    break;
                }

                Console.WriteLine("Processing: {0}", s);
                Thread.Sleep(2000);
                Console.WriteLine("Processed: {0}", s);
            }
        }
    }
}
