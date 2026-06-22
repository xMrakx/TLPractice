using CarFactory.Factories.Enums;
using CarFactory.Models.Engines;

namespace CarFactory.Factories;

public static class EngineFactory
{
    public static IEngine Create( EngineType type )
    {
        return type switch
        {
            EngineType.Gasoline => new GasolineEngine(),
            EngineType.Diesel => new DieselEngine(),
            EngineType.Electric => new ElectricEngine(),
            _ => throw new ArgumentException( $"Неизвестный тип двигателя: {type}" )
        };
    }
}
