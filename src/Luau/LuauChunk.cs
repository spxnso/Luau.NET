using System.Runtime.InteropServices;
using Luau.Native;

namespace Luau
{
    public class LuauChunk(IntPtr pointer, UIntPtr size) : IDisposable
    {
        public IntPtr Pointer { get; private set; } = pointer;
        public UIntPtr Size { get; } = size;

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LuauChunk()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (Pointer != IntPtr.Zero)
            {
                NativeMethods.lua_free(Pointer);
                Pointer = IntPtr.Zero;
            }

            IsDisposed = true;
        }

        public unsafe Span<byte> AsSpan()
        {
            ThrowIfDisposed();

            if ((ulong)Size > int.MaxValue)
                throw new InvalidOperationException("Chunk too large.");

            return new Span<byte>((void*)Pointer, (int)Size);
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, GetType().Name);
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
