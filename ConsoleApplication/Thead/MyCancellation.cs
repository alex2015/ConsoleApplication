using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public static class MyCancellation
    {
        public static void MyMain()
        {
            MyMain1();
        }

        #region MyMain1

        public static async void MyMain1()
        {

            Console.ReadKey();
        }

        public static async Task Go()
        {
            // Создание объекта CancellationTokenSource, отменяющего себя
            // через заданный промежуток времени в миллисекундах
            var cts = new CancellationTokenSource(5000); // Чтобы отменить ранее,
            var ct = cts.Token;                          // вызовите cts.Cancel()
            try
            {
                // Я использую Task.Delay для тестирования; замените другим методом,
                // возвращающим Task
                await Task.Delay(10000).WithCancellation(ct);
                Console.WriteLine("Task completed");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task cancelled");
            }
        }

        private struct Void { } // Из-за отсутствия необобщенного класса TaskCompletionSource.

        private static async Task WithCancellation(this Task originalTask, CancellationToken ct)
        {
            // Создание объекта Task, завершаемого при отмене CancellationToken
            var cancelTask = new TaskCompletionSource<Void>();
            // При отмене CancellationToken завершить Task
            using (ct.Register(t => ((TaskCompletionSource<Void>) t).TrySetResult(new Void()),cancelTask))
            {
                // Создание объекта Task, завершаемого при отмене исходного
                // объекта Task или объекта Task от CancellationToken
                Task any = await Task.WhenAny(originalTask, cancelTask.Task);
                // Если какой-либо объект Task завершается из-за CancellationToken,
                // инициировать OperationCanceledException
                if (any == cancelTask.Task) ct.ThrowIfCancellationRequested();
            }

            // Выполнить await для исходного задания (синхронно);  awaiting it 
            // если произойдет ошибка, выдать первое внутреннее исключение
            // вместо AggregateException
            await originalTask;
        }


        #endregion

    }
}
