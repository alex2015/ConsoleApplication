using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    class MyAsyncView
    {
        private static async Task<int> SumCharactersAsync(IEnumerable<char> text)
        {
            int total = 0;
            foreach (char ch in text)
            {
                int unicode = ch;
                await Task.Delay(unicode);
                total += unicode;
            }
            await Task.Yield();
            return total;
        }
    }
}
