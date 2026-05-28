using System;
using System.Text;
using Luau;

class Program
{
    static void Main(string[] args)
    {
        using var luau = new Luau.Luau();

        try
        {
            luau.OpenLibraries();
            luau.State.Sandbox();

            var chunk = luau.Compile(File.ReadAllText("input.luau"));

            luau.DoChunk(chunk);
        }
        catch (Luau.LuauException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}