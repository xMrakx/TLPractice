namespace CarFactory.Models.Transmissions;

public class AutoTransmission : ITransmission
{
    public string TypeName => "автоматическая";

    public int GearCount => 6;
}
