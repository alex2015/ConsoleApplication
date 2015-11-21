using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyLock
    {
        public static void MyMain()
        {
            WorkerThreadExample.MyMain();
        }

        #region MyMain1

        public static void MyMain1()
        {
            var t = DateTime.Now;
            var t1 = Task.Run(() => Method());
            var t2 = Task.Run(() => Method());

            t1.Wait();
            t2.Wait();
            Console.WriteLine(DateTime.Now - t);

            Console.ReadKey();
        }


        public static void Method()
        {
            Thread.Sleep(10000);
        }


        #endregion


    }

    // Этот класс используется классом LinkedList
    public class Node
    {
        internal Node m_next;
        // Остальные члены не показаны
    }

    internal sealed class ThreadsSharingData
    {
        private volatile Int32 m_flag = 0;
        private Int32 m_value = 0;
        // Этот метод исполняется одним потоком
        public void Thread1()
        {
            // ПРИМЕЧАНИЕ. Значение 5 должно быть записано в m_value
            // перед записью 1 в m_flag
            m_value = 5;
            m_flag = 1;
        }
        // Этот метод исполняется другим потоком
        public void Thread2()
        {
            // ПРИМЕЧАНИЕ. Поле m_value должно быть прочитано после m_flag
            if (m_flag == 1)
            {
                Console.WriteLine(m_value);
            }
        }
    }

    public class Worker
    {
        // This method is called when the thread is started.
        public void DoWork()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("Worker thread: working...");
            }
            Console.WriteLine("Worker thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Keyword volatile is used as a hint to the compiler that this data
        // member is accessed by multiple threads.
        private volatile bool _shouldStop;
    }

    public class WorkerThreadExample
    {
        public static void MyMain()
        {
            // Create the worker thread object. This does not start the thread.
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("Main thread: starting worker thread...");

            // Loop until the worker thread activates.
            while (!workerThread.IsAlive) ;

            // Put the main thread to sleep for 1 millisecond to
            // allow the worker thread to do some work.
            Thread.Sleep(1);

            // Request that the worker thread stop itself.
            workerObject.RequestStop();

            // Use the Thread.Join method to block the current thread 
            // until the object's thread terminates.
            workerThread.Join();
            Console.WriteLine("Main thread: worker thread has terminated.");

            Console.ReadKey();
        }
        // Sample output:
        // Main thread: starting worker thread...
        // Worker thread: working...
        // Worker thread: working...
        // Worker thread: working...
        // Worker thread: working...
        // Worker thread: working...
        // Worker thread: working...
        // Worker thread: terminating gracefully.
        // Main thread: worker thread has terminated.
    }
}
