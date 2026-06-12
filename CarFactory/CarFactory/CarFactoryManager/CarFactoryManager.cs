using System.Diagnostics;
using CarFactory.CarFactoryManager.Constants;
using CarFactory.CarFactoryManager.Enums;
using CarFactory.Extensions;
using CarFactory.Models.Cars;

namespace CarFactory.CarFactoryManager;

public class CarFactoryManager
{
    private bool _isWorking = false;
    private Car _currentCar = null;

    public void Start()
    {
        _isWorking = true;
        Run();
    }

    private void Run()
    {
        while ( _isWorking )
        {
            PrintMenu( MenuConstants.MainMenuOptions );
            string option = Console.ReadLine();
            MenuOptionResult result = MainMenuHandleOption( option );
            if ( result == MenuOptionResult.InvalidInput )
            {
                Console.WriteLine( "Неверная опция" );
            }
        }
    }

    private void PrintMenu( List<string> options )
    {
        foreach ( string option in options )
        {
            Console.WriteLine( option );
        }
        Console.WriteLine();
    }

    private MenuOptionResult MainMenuHandleOption( string option )
    {
        if ( !Enum.TryParse<MainMenuOption>( option, out MainMenuOption menuOption ) )
        {
            return MenuOptionResult.InvalidInput;
        }

        return menuOption switch
        {
            MainMenuOption.AddNewConfig => AddNewConfig(),
            MainMenuOption.ShowCurrentComfig => ShowCurrentConfig(),
            MainMenuOption.Exit => Stop(),
            _ => MenuOptionResult.InvalidInput
        };
    }

    private MenuOptionResult AddNewConfig()
    {
        Car car = CarBuilder.Build();

        _currentCar = car;

        Console.WriteLine( "Новая конфигурация" );
        ShowCurrentConfig();

        return MenuOptionResult.Succsess;
    }

    private MenuOptionResult ShowCurrentConfig()
    {
        if ( _currentCar == null )
        {
            Console.WriteLine( "Отсутсвует конфигурация машины" );

            return MenuOptionResult.Succsess;
        }

        string name = _currentCar.Name;
        string carBodyColor = CarColorExtensions.ColorToString( _currentCar.GetCarColor() );
        string carBody = _currentCar.GetCarBody().TypeName;
        string engine = _currentCar.GetEngine().TypeName;
        string stWheelType = SteeringWheelTypeExtensions.SteeringWheelTypeToString( _currentCar.GetSteeringWheelType() );
        string transmission = _currentCar.GetTransmission().TypeName;
        int gearCount = _currentCar.GetTransmission().GearCount;
        int maxSpeed = _currentCar.CalculateMaxSpeed();

        Console.WriteLine( $"Модель: {name}" );
        Console.WriteLine( $"Корпус: {carBodyColor} {carBody}" );
        Console.WriteLine( $"Двигатель: {engine}" );
        Console.WriteLine( $"Рулевое колесо: {stWheelType}" );
        Console.WriteLine( $"Коробка передач: {transmission}, количество передач: {gearCount}" );
        Console.WriteLine( $"Максимальная скорость: {maxSpeed}" );
        Console.WriteLine();

        return MenuOptionResult.Succsess;
    }

    private MenuOptionResult Stop()
    {
        _isWorking = false;

        return MenuOptionResult.Succsess;
    }
}
