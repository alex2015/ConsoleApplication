using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyWebRequest
    {
        public static void MyMain()
        {
            MyMain1();
        }

        public static void MyMain1()
        {
            var mwr = new MultiWebRequests();


            Console.ReadKey();
        }
    }

    internal enum CoordinationStatus
    {
        Cancel = 1,
        Timeout = 2,
        AllDone = 3
    }

    internal sealed class AsyncCoordinator
    {
        private Int32 m_opCount = 1;        // Уменьшается на 1 методом AllBegun
        private Int32 m_statusReported = 0; // 0=false, 1=true
        private Action<CoordinationStatus> m_callback;
        private Timer m_timer;
        // Этот метод ДОЛЖЕН быть вызван ДО инициирования операции
        public void AboutToBegin(Int32 opsToAdd = 1)
        {
            Interlocked.Add(ref m_opCount, opsToAdd);
        }
        // Этот метод ДОЛЖЕН быть вызван ПОСЛЕ обработки результата
        public void JustEnded()
        {
            if (Interlocked.Decrement(ref m_opCount) == 0)
            {
                ReportStatus(CoordinationStatus.AllDone);
            }
        }
        // Этот метод ДОЛЖЕН быть вызван ПОСЛЕ инициирования ВСЕХ операций
        public void AllBegun(Action<CoordinationStatus> callback, Int32 timeout = Timeout.Infinite)
        {
            m_callback = callback;
            if (timeout != Timeout.Infinite)
            {
                m_timer = new Timer(TimeExpired, null, timeout, Timeout.Infinite);
            }
            JustEnded();
        }
        private void TimeExpired(Object o)
        {
            ReportStatus(CoordinationStatus.Timeout);
        }
        public void Cancel() { ReportStatus(CoordinationStatus.Cancel); }
        private void ReportStatus(CoordinationStatus status)
        {
            // Если состояние ни разу не передавалось, передать его;
            // в противном случае оно игнорируется
            if (Interlocked.Exchange(ref m_statusReported, 1) == 0)
            {
                m_callback(status);
            }
        }
    }

    internal sealed class MultiWebRequests
    {
        // Этот класс Helper координирует все асинхронные операции
        private AsyncCoordinator m_ac = new AsyncCoordinator();
        // Набор веб-серверов, к которым будут посылаться запросы
        // Хотя к этому словарю возможны одновременные обращения,
        // в синхронизации доступа нет необходимости, потому что
        // ключи после создания доступны только для чтения
        private Dictionary<String, Object> m_servers = new Dictionary<String, Object>
        {
            {"http://Wintellect.com/", null},
            {"http://Microsoft.com/", null},
            {"http://1.1.1.1/", null}
        };


        public MultiWebRequests(Int32 timeout = Timeout.Infinite)
        {
            // Асинхронное инициирование всех запросов
            var httpClient = new HttpClient();
            foreach (var server in m_servers.Keys)
            {
                m_ac.AboutToBegin(1);
                httpClient.GetByteArrayAsync(server).ContinueWith(task => ComputeResult(server, task));
            }
            // Сообщаем AsyncCoordinator, что все операции были инициированы
            // и что он должен вызвать AllDone после завершения всех операций,
            // вызова Cancel или тайм-аута
            m_ac.AllBegun(AllDone, timeout);
        }


        private void ComputeResult(String server, Task<Byte[]> task)
        {
            Object result;
            if (task.Exception != null)
            {
                result = task.Exception.InnerException;
            }
            else
            {
                // Обработка завершения ввода-вывода - здесь или в потоке(-ах) пула
                // Разместите свой вычислительный алгоритм...
                result = task.Result.Length;   // В данном примере
            }                                 // просто возвращается длина
            // Сохранение результата (исключение/сумма)
            // и обозначение одной завершенной операции
            m_servers[server] = result;
            m_ac.JustEnded();
        }
        // При вызове этого метода результаты игнорируются
        public void Cancel() { m_ac.Cancel(); }



        // Этот метод вызывается после получения ответа от всех веб-серверов, 
        // вызова Cancel или тайм-аута
        private void AllDone(CoordinationStatus status)
        {
            switch (status)
            {
                case CoordinationStatus.Cancel:
                    Console.WriteLine("Operation canceled.");
                    break;
                case CoordinationStatus.Timeout:
                    Console.WriteLine("Operation timed out.");
                    break;
                case CoordinationStatus.AllDone:
                    Console.WriteLine("Operation completed; results below:");
                    foreach (var server in m_servers)
                    {
                        Console.Write("{0} ", server.Key);
                        Object result = server.Value;
                        if (result is Exception)
                        {
                            Console.WriteLine("failed due to {0}.", result.GetType().Name);
                        }
                        else
                        {
                            Console.WriteLine("returned {0:N0} bytes.", result);
                        }
                    }
                    break;
            }
        }
    }
}
