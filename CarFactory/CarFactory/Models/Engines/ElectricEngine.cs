namespace CarFactory.Models.Engines;

public class ElectricEngine : IEngine
{
    public string TypeName => "электрический";

    public int Power => 10;
}
