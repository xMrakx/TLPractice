using System.Diagnostics;
using CarFactory.CarFactoryManager.Constants;
using CarFactory.CarFactoryManager.Enums;
using CarFactory.Extensions;
using CarFactory.Models.Cars;

namespace CarFactory.CarFactoryManager;

public class CarFactoryManager
{
    private bool _isWorking = false;
    private Car? _currentCar;

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
            MainMenuOption.ShowCurrentConfig => ShowCurrentConfig(),
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

        return MenuOptionResult.Success;
    }

    private MenuOptionResult ShowCurrentConfig()
    {
        if ( _currentCar == null )
        {
            Console.WriteLine( "Отсутсвует конфигурация машины" );

            return MenuOptionResult.Success;
        }
        
        _currentCar.Print();

        return MenuOptionResult.Success;
    }

    private MenuOptionResult Stop()
    {
        _isWorking = false;

        return MenuOptionResult.Success;
    }
}
