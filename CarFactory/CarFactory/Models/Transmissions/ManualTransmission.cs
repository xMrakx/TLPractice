namespace CarFactory.Models.Transmissions;

public class ManualTransmission : ITransmission
{
    public string TypeName => "ручная";

    public int GearCount => 5;
}
