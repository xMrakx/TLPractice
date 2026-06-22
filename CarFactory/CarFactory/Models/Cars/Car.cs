using System.Drawing;
using CarFactory.Models.CarBodies;
using CarFactory.Models.Color;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;

namespace CarFactory.Models.Cars;

public class Car
{
    private readonly ICarBody _carBody;
    private readonly CarColor _color;
    private readonly IEngine _engine;
    private readonly SteeringWheelType _steeringWheelType;
    private readonly ITransmission _transmission;

    public string Name { get; }

    public Car(
        string name,
        ICarBody carBody,
        CarColor color,
        IEngine engine,
        SteeringWheelType steeringWheel,
        ITransmission transmission )
    {
        Name = name;
        _carBody = carBody;
        _color = color;
        _engine = engine;
        _steeringWheelType = steeringWheel;
        _transmission = transmission;
    }

    public ICarBody GetCarBody() => _carBody;
    public CarColor GetCarColor() => _color;
    public IEngine GetEngine() => _engine;
    public SteeringWheelType GetSteeringWheelType() => _steeringWheelType;
    public ITransmission GetTransmission() => _transmission;
    public int CalculateMaxSpeed() => _engine.Power * _transmission.GearCount - _carBody.SpeedModifier;

    public void Print()
    {
        Console.WriteLine( $"Название: {Name}" );
        Console.WriteLine( $"Тип корпуса: {_carBody.GetType().Name}" );
        Console.WriteLine( $"Цвет: {_color}" );
        Console.WriteLine( $"Тип двигателя: {_engine.GetType().Name}" );
        Console.WriteLine( $"Расположение руля: {_steeringWheelType}" );
        Console.WriteLine( $"Тип коробки передач: {_transmission.GetType().Name}" );
        Console.WriteLine( $"Макс. скорость: {CalculateMaxSpeed()}\n" );
    }
}
