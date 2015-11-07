using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyAutomat
    {
        public static void MyMain()
        {
            MyMain1();
        }

        #region MyMain1

        internal sealed class Type1 { }
        internal sealed class Type2 { }
        private static async Task<Type1> Method1Async()
        {
            Thread.Sleep(3000);
            return new Type1();
            /* Асинхронная операция, возвращающая объект Type1 */
        }
        private static async Task<Type2> Method2Async()
        {
            Thread.Sleep(3000);
            return new Type2();
            /* Асинхронная операция, возвращающая объект Type2 */
        }

        public static void MyMain1()
        {
            MyMethodAsync(3);

            Console.ReadKey();
        }

        private static async Task<String> MyMethodAsync(Int32 argument)
        {
            Int32 local = argument;
            try
            {
                await Method1Async();
                for (Int32 x = 0; x < 3; x++)
                {
                    await Method2Async();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Catch");
            }
            finally
            {
                Console.WriteLine("Finally");
            }
            return "Done";
        }

        #endregion

    }
}
