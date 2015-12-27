using System;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    class MyAsyncException
    {
        public static async void MyMain()
        {
            var t = Catcher();

            t.GetAwaiter().GetResult();
            
            Console.ReadKey();
        }

        async static Task Catcher()
        {
            try
            {
                var t = Thrower();

                await t;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                // Исключение будет обработано здесь
            }
        }

        async static Task Thrower()
        {
            throw new Exception();
            await Task.Delay(100);
            throw new Exception();
        }
    }
}
