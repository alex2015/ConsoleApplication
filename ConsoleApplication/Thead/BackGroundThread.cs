using System;
using System.Threading;

namespace ConsoleApplication.Thead
{
    public class BackGroundThread
    {
        public static void MyMain()
        {
            // Создание нового потока (по умолчанию активного)
            Thread t = new Thread(Worker);
            // Превращение потока в фоновый
            t.IsBackground = true;
            t.Start(); // Старт потока
            // В случае активного потока приложение будет работать около 10 секунд
            // В случае фонового потока приложение немедленно прекратит работу
            Console.WriteLine("Returning from Main");
        }

        private static void Worker()
        {
            Thread.Sleep(10000); // Имитация 10 секунд работы
            // Следующая строка выводится только для кода,
            // исполняемого активным потоком
            Console.WriteLine("Returning from Worker");
        }
    }
}
