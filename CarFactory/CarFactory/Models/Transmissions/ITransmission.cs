namespace CarFactory.Models.Transmissions;

public interface ITransmission
{
    public string TypeName { get; }
    public int GearCount { get; }
}
