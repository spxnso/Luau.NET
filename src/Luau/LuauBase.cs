using Luau.Native;

namespace Luau
{
    public abstract class LuauBase(Luau owner, int reference) : IDisposable
    {
        public Luau Owner { get; set; } = owner;
        public bool IsDisposed { get; set; }
        public int Reference { get; set; } = reference;

        ~LuauBase()
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

            if (!Owner.State.IsNull)
                Owner.State.Unref(Reference);

            IsDisposed = true;
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed || Owner.State.IsNull)
                throw new ObjectDisposedException(GetType().Name);
        }

        internal protected void PushReference()
        {
            ThrowIfDisposed();
            Owner.State.RawGetI(LuaConstants.LUA_REGISTRYINDEX, Reference);
        }
    }
}
