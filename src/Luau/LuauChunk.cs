using System.Runtime.InteropServices;
using Luau.Native;

namespace Luau
{
    // TODO: Properly implement IDisposable in order to not be able to use the chunk after it has been disposed. 
    public class LuauChunk(IntPtr pointer, UIntPtr size) : IDisposable
    {
        public IntPtr Pointer { get; set; } = pointer;
        public UIntPtr Size { get; } = size;

        public bool IsDisposed { get; private set; }

        ~LuauChunk()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (Pointer != IntPtr.Zero)
            {
                NativeMethods.free(Pointer);
                Pointer = IntPtr.Zero;
            }

            IsDisposed = true;
        }

        public unsafe Span<byte> AsSpan()
        {
            ThrowIfDisposed();

            return new Span<byte>((void*)Pointer, (int)Size);
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(Pointer == IntPtr.Zero, GetType().Name);
        }

        public byte[] ToByteArray()
        {
            ThrowIfDisposed();

            byte[] bytecode = new byte[(int)Size];
            Marshal.Copy(Pointer, bytecode, 0, (int)Size);

            return bytecode;
        }

    }
}
