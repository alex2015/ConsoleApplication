﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication.Thead
{
    public class MyParallel
    {
        public static void MyMain()
        {
            MyMain3();
        }

        #region MyMain1

        public static void MyMain1()
        {
            Parallel.For(0, 10, i => DoWork(i));
            //var collection = new List<int>();
            //Parallel.ForEach(collection, item => DoWork(item));

            //Parallel.Invoke(DoWork, DoWork, DoWork);
            Console.ReadKey();
        }

        private static void DoWork()
        {
            Thread.Sleep(3000);
        }

        private static void DoWork(int i)
        {
            Thread.Sleep(3000);
        }

        #endregion


        #region MyMain2

        public static void MyMain2()
        {
            try
            {
                DirectoryBytes("qqqqq", "qq", SearchOption.AllDirectories);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions != null ? ex.InnerExceptions.ToString() : ex.ToString());
            }

            Console.ReadKey();
        }

        private static Int64 DirectoryBytes(String path, String searchPattern, SearchOption searchOption)
        {
            //var files = Directory.EnumerateFiles(path, searchPattern, searchOption);
            var files = new List<string> {"qqqqqqq", "rrrrrrrrr"};

            Int64 masterTotal = 0;
            ParallelLoopResult result = Parallel.ForEach<String, Int64>(
                files,
                () =>
                {
                    // localInit: вызывается в момент запуска задания
                    // Инициализация: задача обработала 0 байтов
                    return 0; // Присваивает taskLocalTotal начальное значение 0
                },
                (file, loopState, index, taskLocalTotal) =>
                {
                    // body: Вызывается
                    // один раз для каждого элемента
                    // Получает размер файла и добавляет его к общему размеру
                    Int64 fileLength = 0;
                    FileStream fs = null;
                    try
                    {
                        fs = File.OpenRead(file);
                        fileLength = fs.Length;
                    }
                    catch (IOException ex)
                    {
                        /* Игнорируем файлы, к которым нет доступа */
                    }
                    finally
                    {
                        if (fs != null) fs.Dispose();
                    }

                    if (file == "qqqqqqq")
                    {
                        throw new IOException("qqqqqqqq");
                    }

                    if (file == "rrrrrrrrr")
                    {
                        throw new InvalidCastException("rrrrrrrrr");
                    }


                    return taskLocalTotal + fileLength;
                },
                taskLocalTotal =>
                {
                    // localFinally: Вызывается один раз в конце задания
                    // Атомарное прибавление размера из задания к общему размеру
                    Interlocked.Add(ref masterTotal, taskLocalTotal);
                });

            return masterTotal;
        }

        #endregion


        #region MyMain3

        public static void MyMain3()
        {
            ObsoleteMethods(Assembly.GetCallingAssembly());

            Console.ReadKey();
        }

        [Obsolete]
        public static void ObsoleteMethods(Assembly assembly)
        {
            var query = from type in assembly.GetExportedTypes().AsParallel()
                from method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                let obsoleteAttrType = typeof (ObsoleteAttribute)
                where Attribute.IsDefined(method, obsoleteAttrType)
                orderby type.FullName
                let obsoleteAttrObj = (ObsoleteAttribute) Attribute.GetCustomAttribute(method, obsoleteAttrType)
                select String.Format("Type={0}\nMethod={1}\nMessage={2}\n", type.FullName, method.ToString(),obsoleteAttrObj.Message);

            // Вывод результатов
            foreach (var result in query)
            {
                Console.WriteLine(result);
            }
        }

        #endregion

    }
}
