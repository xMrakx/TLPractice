using Fighters.Core;

namespace Fighters;

public class Program
{
    private static Fighters.Core.Core _core = new();
    public static void Main(string[] args)
    {
        _core.Start();
    }
}