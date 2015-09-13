using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class AddParams
    {
        public int a;
        public int b;

        public AddParams(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }

    public class AddWithThreads
    {
        public static void MyMain()
        {
            AddAsync();
            Console.ReadLine();
        }

        private static async Task AddAsync()
        {
            Console.WriteLine("*****  Adding  with  Thread  objects  *****");
            Console.WriteLine("ID  of  thread  in  Main()  :  {0}", Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 10);

            await Sum(ap);

            Console.WriteLine("Other  thread  is  done!");
            Console.WriteLine("ID  of  thread  in  Main()  :  {0}", Thread.CurrentThread.ManagedThreadId);
        }

        static Task Sum(object data)
        {
            return Task.Run(() =>
            {
                if (data is AddParams)
                {
                    Console.WriteLine("ID  of  thread  in  Add():  {0}",

                    Thread.CurrentThread.ManagedThreadId);

                    AddParams ap = (AddParams)data;

                    Console.WriteLine("{0} + {1} is {2}", ap.a, ap.b, ap.a + ap.b);
                }
            });
        }
    }
}
