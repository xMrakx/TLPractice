using CarFactory.Factories.Enums;
using CarFactory.Models.CarBodies;

namespace CarFactory.Factories;

public static class CarBodyFactory
{
    public static ICarBody Create( CarBodyType type )
    {
        return type switch
        {
            CarBodyType.Sedan => new Sedan(),
            CarBodyType.HatchBack => new Hatchback(),
            CarBodyType.SUV => new SUV(),
            _ => throw new ArgumentException( $"Неизвестный тип корпуса: {type}" )
        };
    }
}
