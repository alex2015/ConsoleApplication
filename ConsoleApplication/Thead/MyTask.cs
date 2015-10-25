using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    internal class MyTask
    {
        public static void MyMain()
        {
            MyMain6();
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

        #region MyMain5

        public static void MyMain5()
        {
            Task<Int32[]> parent = new Task<Int32[]>(() =>
            {
                var results = new Int32[3]; // Создание массива для результатов
                // Создание и запуск 3 дочерних заданий
                new Task(() => results[0] = Sum5(10000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = Sum5(20000), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = Sum5(30000), TaskCreationOptions.AttachedToParent).Start();

                // Возвращается ссылка на массив
                // (элементы могут быть не инициализированы)
                return results;
            });

            // Вывод результатов после завершения родительского и дочерних заданий
            var cwt = parent.ContinueWith(parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));

            // Запуск родительского задания, которое запускает дочерние
            parent.Start();

            Console.ReadKey();
        }

        private static Int32 Sum5(Int32 n)
        {
            Thread.Sleep(1000);
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

        #region MyMain6

        public static void MyMain6()
        {
            Task parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<Int32>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

                // Задание создает и запускает 3 дочерних задания
                var childTasks = new[]
                {
                    tf.StartNew(() => Sum6(cts.Token, 10000)),
                    tf.StartNew(() => Sum6(cts.Token, 20000)),
                    tf.StartNew(() => Sum6(cts.Token, Int32.MaxValue)) // Исключение OverflowException
                };

                // Если дочернее задание становится источником исключения,
                // отменяем все дочерние задания
                for (Int32 task = 0; task < childTasks.Length; task++)
                {
                    childTasks[task].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }

                // После завершения дочерних заданий получаем максимальное
                // возвращенное значение и передаем его другому заданию
                // для вывода
                tf.ContinueWhenAll(childTasks,
                    completedTasks => 
                        completedTasks.Where(t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
                    CancellationToken.None)
                    .ContinueWith(t => Console.WriteLine("The maximum is: " + t.Result),
                        TaskContinuationOptions.ExecuteSynchronously);
            });

            // После завершения дочерних заданий выводим, в том числе, и необработанные исключения
            parent.ContinueWith(p =>
            {
                // Текст помещен в StringBuilder и однократно вызван
                // метод Console.WriteLine просто потому, что это задание
                // может выполняться параллельно с предыдущим,
                // и я не хочу путаницы в выводимом результате
                var sb = new StringBuilder("The following exception(s) occurred:" + Environment.NewLine);
                foreach (var e in p.Exception.Flatten().InnerExceptions)
                {
                    sb.AppendLine(" " + e.GetType().ToString());
                }

                Console.WriteLine(sb.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);

            // Запуск родительского задания, которое может запускать дочерние
            parent.Start();

            Console.ReadKey();
        }

        private static Int32 Sum6(CancellationToken ct, Int32 n)
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
