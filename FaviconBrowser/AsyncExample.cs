using System;
using System.Net;
using System.Threading.Tasks;

namespace FaviconBrowser
{
    internal class AsyncExample
    {
        public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
        {
            TaskCompletionSource<IPHostEntry> tcs = new TaskCompletionSource<IPHostEntry>();
            Dns.BeginGetHostEntry(hostNameOrAddress, asyncResult =>
            {
                try
                {
                    IPHostEntry result = Dns.EndGetHostEntry(asyncResult);
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }, null);
            return tcs.Task;
        }
    }
}
