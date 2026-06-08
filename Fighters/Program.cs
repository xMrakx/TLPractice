using Fighters.FighterManager;

namespace Fighters;

public class Program
{
    private static Fighters.FighterManager.FighterManager _fighterManager = new();
    public static void Main(string[] args)
    {
        _fighterManager.Start();
    }
}