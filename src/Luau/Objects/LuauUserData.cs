namespace Luau.Objects
{
    public sealed class LuauUserData(Luau owner, int reference) : LuauBase(owner, reference)
    {
        public IntPtr Pointer
        {
            get
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                IntPtr ptr = Owner.State.ToUserdata(-1);
                Owner.State.SetTop(stackBase);
                return ptr;
            }
        }

        public int Tag
        {
            get
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                int tag = Owner.State.UserdataTag(-1);
                Owner.State.SetTop(stackBase);
                return tag;
            }
        }
    }
}
