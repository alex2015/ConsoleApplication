using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    internal class MyTask
    {
        public static void MyMain()
        {
            MyMain4();
        }

        #region MyMain1

        public static void MyMain1()
        {
            // Создание задания Task (оно пока не выполняется)
            var t = new Task<Int32>(n => Sum((Int32) n), 100);
            // Можно начать выполнение задания через некоторое время
            t.Start();
            // Можно ожидать завершения задания в явном виде

            try
            {
                t.Wait(); // ПРИМЕЧАНИЕ. Существует перегруженная версия,
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
            }

            // принимающая тайм-аут/CancellationToken
            // Получение результата (свойство Result вызывает метод Wait)
            Console.WriteLine("The Sum is: " + t.Result); // Значение Int32

            Console.ReadKey();
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                checked
                {
                    sum += n;
                } // при больших n появляется
                // исключение System.OverflowException
            }
            return sum;
        }

        #endregion

        #region MyMain2

        public static void MyMain2()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 10000), cts.Token);

            // Можно начать выполнение задания через некоторое время
            t.Start();
            Thread.Sleep(10000);

            // Позднее отменим CancellationTokenSource, чтобы отменить Task
            cts.Cancel(); // Это асинхронный запрос, задача уже может быть завершена


            try
            {
                // В случае отмены задания метод Result генерирует
                // исключение AggregateException
                Console.WriteLine("The sum is: " + t.Result); // Значение Int32
            }
            catch (AggregateException x)
            {
                // Считаем обработанными все объекты OperationCanceledException
                // Все остальные исключения попадают в новый объект AggregateException,
                // состоящий только из необработанных исключений
                x.Handle(e => e is OperationCanceledException);
                // Строка выполняется, если все исключения уже обработаны
                Console.WriteLine("Sum was canceled");
            }

            Console.ReadKey();
        }

        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                // Следующая строка приводит к исключению OperationCanceledException
                // при вызове метода Cancel для объекта CancellationTokenSource,
                // на который ссылается маркер
                ct.ThrowIfCancellationRequested();
                checked
                {
                    sum += n;
                } // при больших n появляется
                // исключение System.OverflowException
            }
            return sum;
        }

        #endregion

        #region MyMain3

        public static void MyMain3()
        {
            // Создание объекта Task с отложенным запуском
            var t = Task.Run(() => Sum(CancellationToken.None, 10000));

            // Метод ContinueWith возвращает объект Task, но обычно
            // он не используется
            Task cwt = t.ContinueWith(task 
                => 
                Console.WriteLine("The sum is: " + task.Result));

            Console.ReadKey();
        }

        #endregion

        #region MyMain4

        public static void MyMain4()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            // Создание и запуск задания с продолжением
            Task<Int32> t = Task.Run(() => Sum4(cts.Token, 10000), cts.Token);
            Thread.Sleep(1000);
            cts.Cancel();
            // Метод ContinueWith возвращает объект Task, но обычно
            // он не используется
            t.ContinueWith(task => Console.WriteLine("The sum is: " + task.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            t.ContinueWith(task => Console.WriteLine("Sum threw: " + task.Exception), TaskContinuationOptions.OnlyOnFaulted);
            t.ContinueWith(task => Console.WriteLine("Sum was canceled"), TaskContinuationOptions.OnlyOnCanceled);

            Console.ReadKey();
        }

        private static Int32 Sum4(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked
                {
                    sum += n;
                } // при больших n появляется
                // исключение System.OverflowException
            }
            return sum;
        }

        #endregion
    }
}
