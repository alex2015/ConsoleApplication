using System;
using System.Collections.Generic;
using ConsoleApplication.Thead;

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



            MyAutomat.MyMain();
        }

    }
}
