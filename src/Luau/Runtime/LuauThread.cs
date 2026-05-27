using Luau.Native;

namespace Luau.Runtime
{
    public sealed class LuauThread(Luau owner, int reference) : LuauBase(owner, reference)
    {
        public LuauStatus Resume(params object?[] args)
        {
            ThrowIfDisposed();

            int stackBase = Owner.State.GetTop();
            PushReference();

            LuaState coroutine = Owner.State.ToThread(-1);
            Owner.State.SetTop(stackBase);

            if (coroutine.IsNull)
                throw new InvalidOperationException("Reference is not a Lua thread.");

            if (args is not null)
            {
                foreach (var arg in args)
                    Owner.PushObject(arg);
                Owner.State.XMove(coroutine, args.Length);
            }

            int argCount = args?.Length ?? 0;
            int status = coroutine.Resume(Owner.State, argCount);
            return (LuauStatus)status;
        }

        public LuauCoStatus Status
        {
            get
            {
                ThrowIfDisposed();

                int stackBase = Owner.State.GetTop();
                PushReference();
                LuaState coroutine = Owner.State.ToThread(-1);
                Owner.State.SetTop(stackBase);

                if (coroutine.IsNull)
                    throw new InvalidOperationException("Reference is not a Lua thread.");

                return (LuauCoStatus)Owner.State.CoStatus(coroutine);
            }
        }
    }
}
