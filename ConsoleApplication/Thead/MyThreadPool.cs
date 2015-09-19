using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ConsoleApplication.Thead
{
    internal class MyThreadPool
    {
        public static void MyMain()
        {
            MyMain1();
        }

        #region example 1

        private static void MyMain1()
        {
            Console.WriteLine("Main thread: queuing an asynchronous operation");

            ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);

            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10000); // Имитация другой работы (10 секунд)
            Console.WriteLine("Hit <Enter> to end this program...");
            Console.ReadLine();
        }

        // Сигнатура метода совпадает с сигнатурой делегата WaitCallback
        private static void ComputeBoundOp(Object state)
        {
            // Метод выполняется потоком из пула
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1000); // Имитация другой работы (1 секунда)
            // После возвращения управления методом поток
            // возвращается в пул и ожидает следующего задания
        }

        #endregion

        #region example 2

        private static void MyMain2()
        {
            // Помещаем данные в контекст логического вызова потока метода Main
            CallContext.LogicalSetData("Name", "Jeffrey");
            // Заставляем поток из пула работать
            // Поток из пула имеет доступ к данным контекста логического вызова
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name={0}", CallContext.LogicalGetData("Name")));
            // Запрещаем копирование контекста исполнения потока метода Main
            ExecutionContext.SuppressFlow();
            // Заставляем поток из пула выполнить работу.
            // Поток из пула НЕ имеет доступа к данным контекста логического вызова
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name={0}", CallContext.LogicalGetData("Name")));
            // Восстанавливаем копирование контекста исполнения потока метода Main
            // на случай будущей работы с другими потоками из пула
            ExecutionContext.RestoreFlow();

            Console.ReadLine();
        }

        #endregion
    }
}
