using CarFactory.CarFactoryManager.Constants;
using CarFactory.Factories;
using CarFactory.Factories.Enums;
using CarFactory.Models.CarBodies;
using CarFactory.Models.Cars;
using CarFactory.Models.Color;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;

namespace CarFactory;

public static class CarBuilder
{
    public static Car Build()
    {
        string name = GetValidName();

        CarBodyType carBodyType = GetValidEnum<CarBodyType>(
            "Выберите тип корпуса",
            MenuConstants.ChooseCarBodyTypeOptions,
            "Неверный выбор типа корпуса"
        );

        CarColor carColor = GetValidEnum<CarColor>(
            "Выберите цвет корпуса",
            MenuConstants.ChooseCarColorOptions,
            "Неверный выбор цвета корпуса"
        );

        EngineType engineType = GetValidEnum<EngineType>(
            "Выберите тип двигателя",
            MenuConstants.ChooseEngineType,
            "Неверный выбор типа двигателя"
        );

        SteeringWheelType steeringWheelType = GetValidEnum<SteeringWheelType>(
            "Выберите расположение руля",
            MenuConstants.ChooseSteeringWheelType,
            "Неверное выбор расположения руля"
        );

        TransmissionType transmissionType = GetValidEnum<TransmissionType>(
            "Выберите тип коробки передач",
            MenuConstants.ChooseTransmissionType,
            "Неверный выбор коробки передач"
        );

        ICarBody carBody = CarBodyFactory.Create( carBodyType );
        IEngine engine = EngineFactory.Create( engineType );
        ITransmission transmission = TransmissionFactory.Create( transmissionType );

        Car car = new(
            name,
            carBody,
            carColor,
            engine,
            steeringWheelType,
            transmission
        );

        return car;
    }


    private static string GetValidName()
    {
        Console.WriteLine( "Введите название конфигурации" );
        string name = String.Empty;
        do
        {
            name = Console.ReadLine();
            if ( String.IsNullOrEmpty( name ) )
            {
                Console.WriteLine( "Название не может быть пустым" );
                Console.WriteLine();
            }
        }
        while ( String.IsNullOrEmpty( name ) );

        return name;
    }

    private static T GetValidEnum<T>(
       string message,
       List<string> menu,
       string errorMessage
       ) where T : Enum
    {
        T result = default;
        do
        {
            Console.WriteLine( message );
            PrintMenu( menu );
            Console.WriteLine();

            string input = Console.ReadLine();
            result = ParseEnum<T>( input );

            if ( result.Equals( default( T ) ) )
            {
                Console.WriteLine( errorMessage );
                Console.WriteLine();
            }
        }
        while ( result.Equals( default( T ) ) );

        return result;
    }

    private static void PrintMenu( List<string> options )
    {
        foreach ( string option in options )
        {
            Console.WriteLine( option );
        }
    }

    private static T ParseEnum<T>( string input ) where T : Enum
    {
        if ( !int.TryParse( input, out int value ) )
        {
            return default( T );
        }

        if ( !Enum.IsDefined( typeof( T ), value ) )
        {
            return default( T );
        }

        T result = ( T )Enum.ToObject( typeof( T ), value );

        return result;
    }
}
