# Compilation

## Compiling Source

```csharp
using var chunk = luau.Compile("return 1 + 1");
```

Or use `LuauCompiler` directly:

```csharp
using var chunk = LuauCompiler.Compile("return 1 + 1");
```

## Compile Options

```csharp
var options = new LuauCompileOptions
{
    OptimizationLevel = 2, // 0 = none, 1 = default, 2 = full
    DebugLevel = 0,        // 0 = none, 1 = default, 2 = full
    TypeInfoLevel = 0,
    CoverageLevel = 0
};

using var chunk = luau.Compile(source, options);
```

## LuauChunk

A compiled chunk owns the native bytecode allocation and must be disposed:

```csharp
using var chunk = luau.Compile(source);

// Access raw bytecode
byte[] bytes = chunk.ToByteArray();
Span<byte> span = chunk.AsSpan();
```

Compilation errors throw `LuauException` with the compiler's error message.