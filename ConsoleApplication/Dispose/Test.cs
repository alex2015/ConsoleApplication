using System;

namespace ConsoleApplication.Dispose
{
    class Test : IDisposable
    {
        // НЕ virtual
        public void Dispose()
        {
            Dispose(true);
            // Препятствует запуску финализатора
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Вызвать метод Dispose на других объектах, которыми владеет данный экземпляр.
                // Здесь можно ссылаться на другие финализируемые объекты.
            }

            // Освободить неуправляемые ресурсы, которыми владеет (только) этот объект.
            // ...
        }

        ~Test()
        {
            Dispose(false);
        }
    }
}
