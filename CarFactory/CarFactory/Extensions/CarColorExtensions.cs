using CarFactory.Models.Color;

namespace CarFactory.Extensions;

public static class CarColorExtensions
{
    public static string ColorToString( this CarColor color )
    {
        return color switch
        {
            CarColor.Red => "красный",
            CarColor.Green => "зеленый",
            CarColor.Blue => "синий",
            _ => throw new ArgumentException()
        };
    }
}
