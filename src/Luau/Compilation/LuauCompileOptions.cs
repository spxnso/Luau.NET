namespace Luau.Compilation
{
    public sealed class LuauCompileOptions
    {
        public int OptimizationLevel { get; init; } = 1;

        public int DebugLevel { get; init; } = 1;

        public int TypeInfoLevel { get; init; } = 0;

        public int CoverageLevel { get; init; } = 0;
    }
}