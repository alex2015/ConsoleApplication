using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyTimer
    {
        public static void MyMain()
        {
            MyMain2();
        }

        #region MyMain1

        private static Timer s_timer;

        public static void MyMain1()
        {
            Console.WriteLine("Checking status every 2 seconds");
            // Создание таймера, который никогда не срабатывает. Это гарантирует,
            // что ссылка на него будет храниться в s_timer,
            // До активизации Status потоком из пула
            s_timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);
            // Теперь, когда s_timer присвоено значение, можно разрешить таймеру
            // срабатывать; мы знаем, что вызов Change в Status не выдаст
            // исключение NullReferenceException
            s_timer.Change(0, Timeout.Infinite);


            Console.ReadKey();
        }

        // Сигнатура этого метода должна соответствовать
        // сигнатуре делегата TimerCallback
        private static void Status(Object state)
        {
            // Этот метод выполняется потоком из пула
            Console.WriteLine("In Status at {0}", DateTime.Now);
            Thread.Sleep(1000); // Имитация другой работы (1 секунда)
            // Заставляем таймер снова вызвать метод через 2 секунды
            s_timer.Change(2000, Timeout.Infinite);
            // Когда метод возвращает управление, поток
            // возвращается в пул и ожидает следующего задания
        }

        #endregion

        #region MyMain2

        public static void MyMain2()
        {
            Console.WriteLine("Checking status every 2 seconds");
            Status();

            Console.ReadKey();
        }

        // Методу можно передавать любые параметры на ваше усмотрение
        private static async void Status()
        {
            while (true)
            {
                Console.WriteLine("Checking status at {0}", DateTime.Now);
                // Здесь размещается код проверки состояния...
                // В конце цикла создается 2-секундная задержка без блокировки потока
                await Task.Delay(2000); // await ожидает возвращения управления потоком
            }
        }

        #endregion

    }
}
