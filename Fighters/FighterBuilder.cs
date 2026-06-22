using Fighters.Core.Constants;
using Fighters.Factories;
using Fighters.Factories.Enums;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public static class FighterBuilder
{
    public static IFighter Build()
    {
        string name = GetValidName();

        FighterType fighterType = GetValidEnum<FighterType>(
            "Выберите класс бойца",
            MenuConstants.ChooseClassOptions,
            "Неверный выбор класса бойца"
        );

        RaceType raceType = GetValidEnum<RaceType>(
            "Выбереите расу бойца",
            MenuConstants.ChooseRaceOptions,
            "Неверный выбор расы бойца"
        );

        WeaponType weaponType = GetValidEnum<WeaponType>(
            "Выберите оружие бойца",
            MenuConstants.ChooseWeaponOptions,
            "Неверный выбор оружия бойца"
        );

        ArmorType armorType = GetValidEnum<ArmorType>(
            "Выберите броню бойца",
            MenuConstants.ChooseArmorOptions,
            "Неверный выбор брони бойца"
        );

        IRace race = RaceFactory.Create( raceType );
        IFighter figher = FighterFactory.Create( fighterType, name, race );
        IWeapon weapon = WeaponFactory.Create( weaponType );
        IArmor armor = ArmorFactory.Create( armorType );

        figher.SetWeapon( weapon );
        figher.SetArmor( armor );

        return figher;
    }

    private static string GetValidName()
    {
        Console.WriteLine( "Введите имя бойца" );
        string name = String.Empty;
        do
        {
            name = Console.ReadLine();
            if ( String.IsNullOrEmpty( name ) )
            {
                Console.WriteLine( "Имя не может быть пустым" );
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
