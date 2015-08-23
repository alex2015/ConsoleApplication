using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApplication.Dispose
{
    internal class ProperComplexResourceHolder : IDisposable
    {
        // Буфер из неуправляемого кода (неуправляемый ресурс)    
        private IntPtr _buffer;
        // Дескриптор события ОС (управляемый ресурс) 
        private SafeHandle _handle;

        public ProperComplexResourceHolder()
        {
            // Захватываем ресурсы       
            _buffer = AllocateBuffer();
            _handle = new SafeWaitHandle(IntPtr.Zero, true);
        }

        protected virtual void DisposeNativeResources()
        {
            ReleaseBuffer(_buffer);
        }

        protected virtual void DisposeManagedResources()
        {
            if (_handle != null)
            {
                _handle.Dispose();
            }
        }

        ~ProperComplexResourceHolder()
        {
            DisposeNativeResources();
        }

        public void Dispose()
        {
            DisposeNativeResources(); 
            DisposeManagedResources(); 
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