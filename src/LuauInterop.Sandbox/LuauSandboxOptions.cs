using LuauInterop.Runtime;

namespace LuauInterop.Sandbox;

public record LuauSandboxOptions
{
    /// <summary>
    /// Libraries to open before sandboxing. Anything not listed here
    /// will be completely absent from the script's environment.
    /// </summary>
    public IReadOnlyList<LuauLibrary> AllowedLibraries { get; init; } = [LuauLibrary.Base, LuauLibrary.Math, LuauLibrary.Table, LuauLibrary.String, LuauLibrary.Bit32, LuauLibrary.Utf8];

    /// <summary>
    /// Allows a library even if a preset denies it.
    /// </summary>
    public LuauSandboxOptions AllowLibrary(LuauLibrary library) => this with
    {
        AllowedLibraries = [.. AllowedLibraries.Concat([library]).Distinct()],
    };

    /// <summary>
    /// Allows libraries even if a preset denies them.
    /// </summary>
    public LuauSandboxOptions AllowLibraries(IReadOnlyList<LuauLibrary> libraries) => this with
    {
        AllowedLibraries = [.. AllowedLibraries.Concat(libraries).Distinct()],
    };

    /// <summary>
    /// Allows a global even if a preset denies it.
    /// </summary>
    public LuauSandboxOptions AllowGlobal(string global) => this with
    {
        DeniedGlobals = [.. DeniedGlobals.Where(g => g != global)],
    };

    /// <summary>
    /// Allows globals even if a preset denies them. 
    /// </summary>
    public LuauSandboxOptions AllowGlobals(IReadOnlyList<string> globals) => this with
    {
        DeniedGlobals = [.. DeniedGlobals.Where(g => !globals.Contains(g))],
    };

    /// <summary>
    /// Additional globals to inject before locking.
    /// Values must be serializable to Luau.
    /// </summary>
    public IReadOnlyDictionary<string, object?> AllowedGlobals { get; init; } = new Dictionary<string, object?>();

    /// <summary>
    /// Globals to remove from the environment even if their library is open.
    /// Useful for stripping individual functions like <c>rawset</c>, <c>setfenv</c>.
    /// </summary>
    public IReadOnlyList<string> DeniedGlobals { get; init; } = [];


    /// <summary>
    /// Denies a global even if a preset allows it.
    /// </summary>
    public LuauSandboxOptions DenyGlobal(string global) => this with
    {
        DeniedGlobals = [.. DeniedGlobals.Concat([global]).Distinct()],
    };

    /// <summary>
    /// Denies globals even if a preset allows them.
    /// </summary>
    public LuauSandboxOptions DenyGlobals(IReadOnlyList<string> globals) => this with
    {
        DeniedGlobals = [.. DeniedGlobals.Concat(globals).Distinct()],
    };

    /// <summary>
    /// Whether each <see cref="LuauSandbox.Execute"/> call gets its own
    /// proxied <c>_G</c> via <c>luaL_sandboxthread</c>.
    /// Disabling allows scripts to share globals across calls (not recommended).
    /// </summary>
    public bool IsolateScripts { get; init; } = true;

    /// <summary>
    /// Whether to call <c>luaL_sandbox</c> to make all builtins read-only.
    /// Disabling this is not recommended.
    /// </summary>
    public bool LockGlobals { get; init; } = true;

    /// <summary>
    /// Whether to remove debug info using compile options. Disabling this is not recommended.
    /// </summary>
    public bool StripDebugInfo { get; init; } = true;

    /// <summary>
    /// Default sandbox options.
    /// </summary>
    public static LuauSandboxOptions Default { get; } = new();

    /// <summary>
    /// Strict preset for fully untrusted code.
    /// </summary>
    public static LuauSandboxOptions Strict { get; } = new()
    {
        AllowedLibraries = [LuauLibrary.Base, LuauLibrary.Math, LuauLibrary.Table, LuauLibrary.String, LuauLibrary.Bit32],
        DeniedGlobals = ["setfenv", "getfenv", "rawset", "rawget", "rawequal", "rawlen", "newproxy", "collectgarbage",],
        LockGlobals = true,
        IsolateScripts = true,
    };
}