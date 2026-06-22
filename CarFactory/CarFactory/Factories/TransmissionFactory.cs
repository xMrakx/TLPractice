using CarFactory.Factories.Enums;
using CarFactory.Models.Transmissions;

namespace CarFactory.Factories;

public static class TransmissionFactory
{
    public static ITransmission Create( TransmissionType type )
    {
        return type switch
        {
            TransmissionType.Manual => new ManualTransmission(),
            TransmissionType.Auto => new AutoTransmission(),
            _ => throw new ArgumentException( $"Неизвестный тип коробки передач: {type}" )
        };
    }
}
