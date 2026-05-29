# Resource Management

All reference-backed wrappers (`LuauFunction`, `LuauTable`, `LuauUserData`, `LuauThread`) hold a Lua registry reference and must be disposed when no longer needed. Failing to dispose them leaks registry slots in the VM.

## Best Practice

Always use `using`:

```csharp
var fn = luau["myFunc"] as LuauFunction;
using (fn)
{
    fn.Call();
}
```

Or `using` declarations:

```csharp
using var fn = luau["myFunc"] as LuauFunction;
fn.Call();
```

## Cross-VM Usage

Passing a reference-backed object to a different `Luau` instance throws `InvalidOperationException`:

```csharp
using var luauA = new Luau.Luau();
using var luauB = new Luau.Luau();

luauA.DoString("function f() end");
var fn = luauA["f"] as LuauFunction;

luauB["f"] = fn; // throws — cross-VM usage is not allowed
```

## VM Disposal

Disposing the `Luau` instance closes the native state. Any wrappers still alive after that are inert — their `IsDisposed` flag reflects the owner's disposal state.

```csharp
var fn = luau["myFunc"] as LuauFunction;
luau.Dispose();

fn.Call(); // throws ObjectDisposedException
```