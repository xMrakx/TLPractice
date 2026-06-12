namespace CarFactory;

public class Program
{
    private static CarFactoryManager.CarFactoryManager _carfactoryManager = new();
    public static void Main( string[] args)
    {
        _carfactoryManager.Start();
    }
}