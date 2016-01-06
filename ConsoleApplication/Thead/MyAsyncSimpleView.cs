using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    class MyAsyncSimpleView
    {
        private static async Task<int> SumCharactersAsync(int a)
        {
            int total = 5;
            await Task.Delay(1000);
            return total + a;
        }
    }
}
