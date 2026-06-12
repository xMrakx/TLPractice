namespace CarFactory.Models.CarBodies;

public interface ICarBody
{
    public string TypeName { get; }
    public int SpeedModifier { get; }
}
