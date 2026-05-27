using Luau.Native;

namespace Luau
{
    public sealed class LuauChunk(IntPtr pointer, UIntPtr size) : IDisposable
    {
        public IntPtr Pointer { get; set; } = pointer;
        public UIntPtr Size { get; } = size;

        public void Dispose()
        {
            if (Pointer == IntPtr.Zero)
                return;

            NativeMethods.free(Pointer);
            Pointer = IntPtr.Zero;

            GC.SuppressFinalize(this);
        }
    }
}