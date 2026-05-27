namespace Luau.Objects
{
    public sealed class LuauTable(Luau owner, int reference) : LuauBase(owner, reference)
    {
        public object? this[string key]
        {
            get
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                Owner.State.GetField(-1, key);
                object? value = Owner.GetObject(-1);
                Owner.State.SetTop(stackBase);
                return value;
            }
            set
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                Owner.PushObject(value);
                Owner.State.SetField(-2, key);
                Owner.State.SetTop(stackBase);
            }
        }

        public object? this[int key]
        {
            get
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                Owner.State.RawGetI(-1, key);
                object? value = Owner.GetObject(-1);
                Owner.State.SetTop(stackBase);
                return value;
            }
            set
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                Owner.PushObject(value);
                Owner.State.RawSetI(-2, key);
                Owner.State.SetTop(stackBase);
            }
        }
    }
}
