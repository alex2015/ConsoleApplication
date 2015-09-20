using System;
using System.Threading;

namespace ConsoleApplication.Thead
{
    public class CancellationDemo
    {
        public static void MyMain()
        {
            MyMain3();
        }

        #region example1

        public static void MyMain1()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            // Передаем операции CancellationToken и число
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));
            Console.WriteLine("Press <Enter> to cancel the operation.");
            Console.ReadLine();
            cts.Cancel(); // Если метод Count уже вернул управления,
            // Cancel не оказывает никакого эффекта
            // Cancel немедленно возвращает управление, метод продолжает работу...
            Console.ReadLine();
        }

        private static void Count(CancellationToken token, Int32 countTo)
        {
            for (Int32 count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break; // Выход их цикла для остановки операции
                }

                Console.WriteLine(count);
                Thread.Sleep(200); // Для демонстрационных целей просто ждем
            }

            Console.WriteLine("Count is done");
        }

        #endregion

        #region example2

        public static void MyMain2()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("Canceled 1"));
            cts.Token.Register(() => Console.WriteLine("Canceled 2"));
            // Для проверки отменим его и выполним оба обратных вызова
            cts.Cancel();
            Console.ReadLine();
        }

        #endregion

        #region example3

        public static void MyMain3()
        {
            // Создание объекта CancellationTokenSource
            var cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => Console.WriteLine("cts1 canceled"));

            // Создание второго объекта CancellationTokenSource
            var cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => Console.WriteLine("cts2 canceled"));

            // Создание нового объекта CancellationTokenSource,
            // отменяемого при отмене cts1 или ct2
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
            linkedCts.Token.Register(() => Console.WriteLine("linkedCts canceled"));

            // Отмена одного из объектов CancellationTokenSource (я выбрал cts2)
            cts2.Cancel();

            // Показываем, какой из объектов CancellationTokenSource был отменен
            Console.WriteLine("cts1 canceled={0}, cts2 canceled={1}, linkedCts={2}", cts1.IsCancellationRequested,
                cts2.IsCancellationRequested,
                linkedCts.IsCancellationRequested);

            Console.ReadLine();
        }

        #endregion
    }
}
