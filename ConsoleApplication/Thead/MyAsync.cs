using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyAsync
    {
        public static void MyMain()
        {
            MyMain2();
        }

        #region MyMain1

        public static void MyMain1()
        {
            Task<String> q = IssueClientRequestAsync(".", "testpipe");

            Console.WriteLine(q.Result);

            Console.ReadKey();
        }

        private static async Task<String> IssueClientRequestAsync(String serverName, String message)
        {
            using (var pipe = new NamedPipeClientStream(serverName, "PipeName", PipeDirection.InOut,
                PipeOptions.Asynchronous | PipeOptions.WriteThrough))
            {
                pipe.Connect(); // Прежде чем задавать ReadMode, необходимо

                pipe.ReadMode = PipeTransmissionMode.Message; // вызвать Connect

                // Асинхронная отправка данных серверу
                Byte[] request = Encoding.UTF8.GetBytes(message);

                await pipe.WriteAsync(request, 0, request.Length);
                // Асинхронное чтение ответа сервера
                Byte[] response = new Byte[1000];

                Int32 bytesRead = await pipe.ReadAsync(response, 0, response.Length);

                return Encoding.UTF8.GetString(response, 0, bytesRead);
            } // Закрытие канала
        }

        #endregion

        #region MyMain2

        public static void MyMain2()
        {
            Console.WriteLine("1: " + Thread.CurrentThread.ManagedThreadId);
            Task<String> q = IssueFileRequestAsync("myfile.txt", "НОВЕЙШАЯ СТРОКА");
            Console.WriteLine("2: " + Thread.CurrentThread.ManagedThreadId);
            var w = q.Result;
            Console.WriteLine("3: " + Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static async Task<String> IssueFileRequestAsync(String filePath, String message)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                // Асинхронная отправка данных серверу
                Byte[] request = Encoding.UTF8.GetBytes(message);

                Console.WriteLine("4: " + Thread.CurrentThread.ManagedThreadId);

                await stream.WriteAsync(request, 0, request.Length);
                // Асинхронное чтение ответа сервера
                Byte[] response = new Byte[1000];

                Console.WriteLine("5: " + Thread.CurrentThread.ManagedThreadId);

                Int32 bytesRead = await stream.ReadAsync(response, 0, response.Length);

                Console.WriteLine("6: " + Thread.CurrentThread.ManagedThreadId);

                return Encoding.UTF8.GetString(response, 0, bytesRead);
            } // Закрытие канала
        }

        #endregion


    }

    internal class BaseClass
    {
        public virtual async void AlexsMethod()
        {
        }
    }

    internal class SubClass : BaseClass
    {
        // Переопределяет метод базового класса AlexsMethod
        public override void AlexsMethod()
        {

        }
    }
}
