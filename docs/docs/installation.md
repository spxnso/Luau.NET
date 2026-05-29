# Installation

LuauInterop is split into two packages:

| Package | Description |
|---------|-------------|
| `LuauInterop` | The managed bindings |
| `LuauInterop.Native` | The native Luau runtime libraries |

Install both:

```bash
dotnet add package LuauInterop
dotnet add package LuauInterop.Native
```

### Supported Platforms

| Platform | Runtime ID |
|----------|------------|
| Windows x64 | `win-x64` |
| Linux x64 | `linux-x64` |
| macOS x64 | `osx-x64` |