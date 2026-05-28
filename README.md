<h1 align="center">
  <br>
    <img src="./assets/icon.png" width="200" />
  <br>
      LuauInterop
  <br>
</h1>

<h4 align="center">Luau bindings for .NET</h4>

Bridge between Luau and DotNet. **LuauInterop** allows you to use [Luau](https://luau.org) directly from C#.

> ⚠️ **Status**: This project is still in active development.
>
> This project is not meant to be published on a package manager until it becomes stable, as we are still looking for bugs and potential memory leaks.

## Installation

Clone the repository and build from source:

```bash
git clone https://github.com/spxnso/Luau.NET.git
cd Luau.NET
```

## Getting Started

### Creating a Luau State

```csharp
using LuauInterop;

// Create a new Luau state
var luau = new Luau();

// Don't forget to dispose when done
using (luau) 
{
    // Your code here
}
```

### Executing Luau Code

```csharp
using LuauInterop;

using (var luau = new Luau())
{
    // Execute a simple expression
    var result = luau.DoString("return 10 + 3 * (5 + 2)");
    double sum = (double)result[0]; // 55
}
```

### Opening Standard Libraries

Luau provides various standard libraries. Open them before using:

```csharp
using (var luau = new Luau())
{
    // Open all standard libraries
    luau.OpenLibraries();
    
    // Or open specific libraries
    // luau.OpenBase();
    // luau.OpenString();
    // luau.OpenMath();
    // luau.OpenTable();
    // luau.OpenCoroutine();
    // etc.
}
```

I will add documentation soon.

## License

LuauInterop is licensed under the MIT License. See [LICENSE](LICENSE) for details.

Luau is provided under the MIT License. See [external/luau/LICENSE.txt](external/luau/LICENSE.txt) for details.