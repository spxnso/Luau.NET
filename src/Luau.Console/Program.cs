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

            luau.DoString(File.ReadAllText("input.luau"));

        }
        catch (Luau.LuauException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}