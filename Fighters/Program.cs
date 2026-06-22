using FM = Fighters.FighterManager.FighterManager;

namespace Fighters;

public class Program
{
    private FM _fighterManager;

    public Program()
    {
        _fighterManager = new FM();
    }

    public static void Main()
    {
        Program program = new Program();
        program._fighterManager.Start();
    }
}