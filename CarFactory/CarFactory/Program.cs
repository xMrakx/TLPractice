using CM = CarFactory.CarFactoryManager.CarFactoryManager;

namespace CarFactory;

public class Program
{
    private  CM _carfactoryManager;

    public Program()
    {
        _carfactoryManager = new CM();
    }   

    public static void Main()
    {
        var program = new Program();
        program._carfactoryManager.Start();
    }
}