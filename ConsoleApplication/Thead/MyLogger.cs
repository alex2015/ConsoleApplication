﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyLogger
    {
        public static void MyMain()
        {
            MyMain1();
        }

        #region MyMain1

        public static void MyMain1()
        {
            Go();

            Console.ReadKey();
        }

        public static async Task Go()
        {
#if DEBUG
            // Использование TaskLogger приводит к лишним затратам памяти
            // и снижению производительности; включить для отладочной версии
            TaskLogger.LogLevel = TaskLogger.TaskLogLevel.Pending;
#endif
            // Запускаем 3 задачи; для тестирования TaskLogger их продолжительность
            // задается явно.
            var tasks = new List<Task>
            {
                Task.Delay(2000).Log("2s op"),
                Task.Delay(5000).Log("5s op"),
                Task.Delay(6000).Log("6s op")
            };
            try
            {
                // Ожидание всех задач с отменой через 3 секунды; только одна задача
                // должна завершиться в указанное время.
                // Примечание: WithCancellation - мой метод расширения,
                // описанный позднее в этой главе.
                //await Task.WhenAll(tasks).WithCancellation(new CancellationTokenSource(3000).Token);
            }
            catch (OperationCanceledException)
            {
            }
            // Запрос информации о незавершенных задачах и их сортировка
            // по убыванию продолжительности ожидания
            foreach (var op in TaskLogger.GetLogEntries().OrderBy(tle => tle.LogTime))
            {
                Console.WriteLine(op);
            }
        }

        #endregion

        public static void Go2()
        {
            ShowExceptions();
            for (Int32 x = 0; x < 3; x++)
            {
                try
                {
                    switch (x)
                    {
                        case 0:
                            throw new InvalidOperationException();
                        case 1:
                            throw new ObjectDisposedException("");
                        case 2:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch
                {
                }
            }
        }

        private static async void ShowExceptions()
        {
            var eventAwaiter = new EventAwaiter<FirstChanceExceptionEventArgs>();
            var q = await eventAwaiter;

            AppDomain.CurrentDomain.FirstChanceException += eventAwaiter.EventRaised;
            while (true)
            {
                Console.WriteLine("AppDomain exception: {0}", (await eventAwaiter).Exception.GetType());
            }
        }
    }

    public static class TaskLogger
    {
        public enum TaskLogLevel
        {
            None,
            Pending
        }

        public static TaskLogLevel LogLevel { get; set; }

        public sealed class TaskLogEntry
        {
            public Task Task { get; internal set; }
            public String Tag { get; internal set; }
            public DateTime LogTime { get; internal set; }
            public String CallerMemberName { get; internal set; }
            public String CallerFilePath { get; internal set; }
            public Int32 CallerLineNumber { get; internal set; }

            public override string ToString()
            {
                return String.Format("LogTime={0}, Tag={1}, Member={2}, File={3}({4})",
                    LogTime, Tag ?? "(none)", CallerMemberName, CallerFilePath,
                    CallerLineNumber);
            }
        }

        private static readonly ConcurrentDictionary<Task, TaskLogEntry> s_log =
            new ConcurrentDictionary<Task, TaskLogEntry>();

        public static IEnumerable<TaskLogEntry> GetLogEntries()
        {
            return s_log.Values;
        }

        public static Task<TResult> Log<TResult>(this Task<TResult> task,
            String tag = null,
            [CallerMemberName] String callerMemberName = null,
            [CallerFilePath] String callerFilePath = null,
            [CallerLineNumber] Int32 callerLineNumber = 1)
        {
            return (Task<TResult>) Log((Task) task, tag, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static Task Log(this Task task, String tag = null,
            [CallerMemberName] String callerMemberName = null,
            [CallerFilePath] String callerFilePath = null,
            [CallerLineNumber] Int32 callerLineNumber = 1)
        {
            if (LogLevel == TaskLogLevel.None) return task;
            var logEntry = new TaskLogEntry
            {
                Task = task,
                LogTime = DateTime.Now,
                Tag = tag,
                CallerMemberName = callerMemberName,
                CallerFilePath = callerFilePath,
                CallerLineNumber = callerLineNumber
            };
            s_log[task] = logEntry;
            task.ContinueWith(t =>
            {
                TaskLogEntry entry;
                s_log.TryRemove(t, out entry);
            }, TaskContinuationOptions.ExecuteSynchronously);

            return task;
        }
    }




    public sealed class EventAwaiter<TEventArgs> : INotifyCompletion
    {
        private ConcurrentQueue<TEventArgs> m_events = new ConcurrentQueue<TEventArgs>();
        private Action m_continuation;

        // Конечный автомат сначала вызывает этот метод для получения
        // объекта ожидания; возвращаем текущий объект
        public EventAwaiter<TEventArgs> GetAwaiter()
        {
            return this;
        }

        // Сообщает конечному автомату, произошли ли какие-либо события
        public Boolean IsCompleted
        {
            get { return m_events.Count > 0; }
        }

        // Конечный автомат сообщает, какой метод должен вызываться позднее;
        // сохраняем полученную информацию
        public void OnCompleted(Action continuation)
        {
            Volatile.Write(ref m_continuation, continuation);
        }

        // Конечный автомат запрашивает результат, которым является
        // результат оператора await
        public TEventArgs GetResult()
        {
            TEventArgs e;
            m_events.TryDequeue(out e);
            return e;
        }

        // Теоретически может вызываться несколькими потоками одновременно,
        // когда каждый поток инициирует событие
        public void EventRaised(Object sender, TEventArgs eventArgs)
        {
            m_events.Enqueue(eventArgs); // Сохранение EventArgs
            // для возвращения из GetResult/await
            // Если имеется незавершенное продолжение, поток забирает его
            Action continuation = Interlocked.Exchange(ref m_continuation, null);
            if (continuation != null) continuation(); // Продолжение выполнения
        } // конечного автомата
    }
}
