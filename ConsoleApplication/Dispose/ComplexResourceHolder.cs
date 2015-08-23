using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApplication.Dispose
{
    public class ComplexResourceHolder : IDisposable
    {
        // Буфер из неуправляемого кода (неуправляемый ресурс)    
        private IntPtr _buffer;
        // Дескриптор события ОС (управляемый ресурс)   
        private SafeHandle _handle;

        public ComplexResourceHolder()
        {
            // Захватываем ресурсы       
            _buffer = AllocateBuffer();
            _handle = new SafeWaitHandle(IntPtr.Zero, true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Неуправляемые ресурсы освобождаются в любом случае       
            ReleaseBuffer(_buffer);                   
            // Вызываем из метода Dispose, освобождаем управляемые ресурсы 
            if (disposing)
            {
                if (_handle != null)
                {
                    _handle.Dispose();
                }
            }
        }

        ~ComplexResourceHolder()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IntPtr AllocateBuffer()
        {
            return new IntPtr();
        }

        private void ReleaseBuffer(IntPtr buffer)
        {
        }
    }
}
