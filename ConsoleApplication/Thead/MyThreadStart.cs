using System;
using System.Threading;

namespace ConsoleApplication.Thead
{
    public class MyThreadStart
    {
        public static void MyMain()
        {
            Console.WriteLine("Main thread: starting a dedicated thread " + "to do an asynchronous operation");
            Thread dedicatedThread = new Thread(ComputeBoundOp);
            dedicatedThread.Start(5);

            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10000);    // Имитация другой работы (10 секунд)
            dedicatedThread.Join(); // Ожидание завершения потока

            Console.WriteLine("Hit <Enter> to end this program...");
            Console.ReadLine();
        }

        private static void ComputeBoundOp(Object state)
        {
            // Метод, выполняемый выделенным потоком
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1000); // Имитация другой работы (1 секунда)
            // После возвращения методом управления выделенный поток завершается
        }
    }
}
