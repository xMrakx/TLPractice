namespace CarFactory.Models.Engines;

public interface IEngine
{
    public string TypeName { get; }
    public int Power { get; }
}
