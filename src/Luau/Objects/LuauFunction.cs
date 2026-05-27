using Luau.Runtime;

namespace Luau.Objects
{
    public sealed class LuauFunction(Luau owner, int reference) : LuauBase(owner, reference)
    {
        public object?[] Call(params object?[] arguments)
        {
            ThrowIfDisposed();

            int stackBase = Owner.State.GetTop();
            PushReference();

            if (arguments is not null)
            {
                foreach (var arg in arguments)
                    Owner.PushObject(arg);
            }

            int argCount = arguments?.Length ?? 0;
            LuauStatus callStatus = (LuauStatus)Owner.State.PCall(argCount, -1, 0);
            if (callStatus != LuauStatus.OK)
                Owner.ThrowLastError();

            int resultCount = Owner.State.GetTop() - stackBase;

            var results = new object?[resultCount];

            for (int i = 0; i < resultCount; i++)
            {
                results[i] = Owner.GetObject(stackBase + i + 1);
            }

            Owner.Pop(resultCount);
            
            return results;
        }
    }
}
