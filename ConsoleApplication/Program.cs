using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleApplication
{
    internal class Program
    {
        public delegate int BinaryOp(int x, int y);

        public event BinaryOp myEvent;

        private static List<string> readLines = new List<string>
        {
            "a",
            "b",
            "c",
            "d"
        };

        public static IEnumerable<string> ReadFromFile(string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            foreach (string line in readLines)
            {
                yield return line;
            }
        }

        private static void Main(string[] args)
        {
            //// Где будет ошибка?
            //var result = ReadFromFile(null); //1
            //Console.WriteLine(1); 
            //foreach (var l in result)
            //{
            //    Console.WriteLine(2); //2
            //}



            //var x = new { Items = new List<int> { 1, 2, 3 }.GetEnumerator() };
            //while (x.Items.MoveNext())
            //{
            //    Console.WriteLine(x.Items);
            //}




        }

        // Сигнатура метода должна совпадать 
        // с сигнатурой делегата ParameterizedThreadStart
        private static void ComputeBoundOp(Object state)
        {
            // Метод, выполняемый выделенным потоком
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1000); // Имитация другой работы (1 секунда)
            // После возвращения методом управления выделенный поток завершается
        }
    }


}
