namespace CarFactory.Models.Engines;

public class DieselEngine : IEngine
{
    public string TypeName => "дизельный";

    public int Power => 30;
}
