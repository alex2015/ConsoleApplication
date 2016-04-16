using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.MessageQueueNamespace
{
    class MyMessageQueue
    {
        public static void MyMain()
        {
            MyMain1();

            Console.ReadKey();
        }


        public static void MyMain1()
        {
            try
            {
                if (!MessageQueue.Exists(@".\private$\MyNewPrivateQueue"))
                {
                    MessageQueue.Create(@".\private$\MyNewPrivateQueue");
                }

                var queue = new MessageQueue(@".\private$\MyNewPrivateQueue");
                queue.Send("Sample Message", "Label");
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
