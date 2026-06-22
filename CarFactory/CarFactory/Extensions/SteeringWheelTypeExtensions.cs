using CarFactory.Models.SteeringWheels;

namespace CarFactory.Extensions;

public static class SteeringWheelTypeExtensions
{
    public static string SteeringWheelTypeToString( this SteeringWheelType type )
    {
        return type switch
        {
            SteeringWheelType.Left => "Левое",
            SteeringWheelType.Right => "Правое",
            _ => throw new ArgumentException()
        };
    }

}
