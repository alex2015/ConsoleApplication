using System;

namespace ConsoleApplication.Observer
{
    public class Singleton
    {
        private static readonly Singleton _instance = new Singleton();
        public static Singleton Instance { get { return _instance; } }
        public event EventHandler Event;
    }

    class MemoryLeak
    {
        public MemoryLeak()
        {
            Singleton.Instance.Event += (s, e) => Console.WriteLine("Hello, Memory Leak!");
        }
    }
}
