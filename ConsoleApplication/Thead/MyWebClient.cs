using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyWebClient
    {
        public static void MyMain()
        {
            MyMain2();
        }

        #region MyMain1

        public static async void MyMain1()
        {
            var t = AwaitWebClient(new Uri("https://www.microsoft.com/ru-ru/"));


            Console.WriteLine(t.Result);
            Console.ReadKey();
        }

        private static async Task<String> AwaitWebClient(Uri uri)
        {
            // Класс System.Net.WebClient поддерживает событийную модель
            // асинхронного программирования
            var wc = new System.Net.WebClient();
            // Создание объекта TaskCompletionSource и его внутреннего объекта Task
            var tcs = new TaskCompletionSource<String>();
            // При завершении загрузки строки объект WebClient инициирует
            // событие DownloadStringCompleted, завершающее TaskCompletionSource
            wc.DownloadStringCompleted += (s, e) =>
            {
                if (e.Cancelled) tcs.SetCanceled();
                else if (e.Error != null) tcs.SetException(e.Error);
                else tcs.SetResult(e.Result);
            };
            // Начало асинхронной операции
            wc.DownloadStringAsync(uri);
            // Теперь мы можем взять объект Task из TaskCompletionSource
            // и обработать результат обычным способом.
            String result = await tcs.Task;
            // Обработка строки результата (если нужно)...
            return result;
        }

        #endregion


        #region MyMain2

        public static async void MyMain2()
        {
            Task.Run(async () =>
            {
                // Этот код выполняется в потоке из пула
                // TODO: Подготовительные вычисления...
                await myMethodAsync();  // Инициирование асинхронной операции
                // Продолжение обработки...
            });



            Console.ReadKey();
        }

        private static async Task<string> myMethodAsync()
        {
            return string.Empty;
        }

        #endregion


        #region MyMain3

        public static async void MyMain3()
        {


            Console.ReadKey();
        }

        static async Task OuterAsyncFunction()
        {
            var q = InnerAsyncFunction();  // В этой строке пропущен оператор await!
            // Код продолжает выполняться, как и InnerAsyncFunction...
        }
        static async Task InnerAsyncFunction() { /* ... */ }

        #endregion

    }
}
