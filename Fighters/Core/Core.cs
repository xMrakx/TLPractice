using Fighters.Core.Constants;
using Fighters.Core.Enums;
using Fighters.Factories;
using Fighters.Factories.Enums;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Core;

public class Core
{
    private bool _isWorking;
    private Arena _arena = new();

    public void Start()
    {
        _isWorking = true;
        Run();
    }

    private void Run()
    {
        Console.WriteLine( "ULTIMATE FIGHTER ARENA SIMULATOR" );
       
        while ( _isWorking )
        {
            PrintMenu( MenuConstants.MainMenuOptions );
            string option = Console.ReadLine();
            MenuOptionResult result = MainMenuHandleOption( option );
            if ( result == MenuOptionResult.InvalidInput )
            {
                //Console.WriteLine();
                Console.WriteLine( "Неверная опция" );
            }
        }
    }

    private void PrintMenu( List<string> options )
    {
        Console.WriteLine();
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

        switch ( menuOption )
        {
            case MainMenuOption.AddFighter:
                return AddFighter();
            case MainMenuOption.StartFight:
                return StartFight();
            case MainMenuOption.Exit:
                return Stop();
            default:
                return MenuOptionResult.InvalidInput;
        }
    }

    private MenuOptionResult AddFighter()
    {
        string name = null;
        while ( string.IsNullOrEmpty( name ) )
        {
            name = GetName();
        }

        FighterType fighterType = FighterType.None;
        while ( fighterType == FighterType.None )
        {
            fighterType = GetFighterType();
        }

        RaceType raceType = RaceType.None;
        while ( raceType == RaceType.None )
        {
            raceType = GetRaceType();
        }

        WeaponType weaponType = WeaponType.None;
        while ( weaponType == WeaponType.None )
        {
            weaponType = GetWeaponType();
        }

        ArmorType armorType = ArmorType.None;
        while ( armorType == ArmorType.None )
        {
            armorType = GetArmorType();
        }

        IRace race = RaceFactory.Create( raceType );
        IFighter figher = FighterFactory.Create( fighterType, name, race );
        IWeapon weapon = WeaponFactory.Create( weaponType );
        IArmor armor = ArmorFactory.Create( armorType );

        figher.SetWeapon( weapon );
        figher.SetArmor( armor );

        _arena.AddFighter( figher );

        return MenuOptionResult.Succses;
    }

    private string GetName()
    {
        Console.WriteLine( "Введите имя бойца" );
        Console.WriteLine();
        string name = Console.ReadLine();
        if ( string.IsNullOrEmpty( name ) )
        {
            Console.WriteLine( "Имя не может быть пустым" );
            Console.WriteLine();
            return null;
        }
        Console.WriteLine();
        return name;
    }

    private FighterType GetFighterType()
    {
        Console.WriteLine( "Выберите класс бойца" );
        PrintMenu( MenuConstants.ChooseClassOptions );
        string option = Console.ReadLine();

        if ( !Enum.TryParse<FighterType>( option, out FighterType fighterType ) ||
             !Enum.IsDefined( typeof( FighterType ), fighterType ) ||
             fighterType == FighterType.None )
        {
            Console.WriteLine( "Неверный выбор класса бойца" );
            Console.WriteLine();
            return FighterType.None;
        }

        return fighterType;
    }

    private RaceType GetRaceType()
    {
        Console.WriteLine( "Выбереите расу бойца" );
        PrintMenu( MenuConstants.ChooseRaceOptions );
        string option = Console.ReadLine();

        if ( !Enum.TryParse<RaceType>( option, out RaceType raceType ) ||
             !Enum.IsDefined( typeof( RaceType ), raceType ) ||
             raceType == RaceType.None )
        {
            Console.WriteLine( "Неверный выбор расы бойца" );
            Console.WriteLine();
            return RaceType.None;
        }

        return raceType;
    }

    private WeaponType GetWeaponType()
    {
        Console.WriteLine( "Выберите оружие бойца" );
        PrintMenu( MenuConstants.ChooseWeaponOptions );
        string option = Console.ReadLine();

        if ( !Enum.TryParse<WeaponType>( option, out WeaponType weaponType ) ||
             !Enum.IsDefined( typeof( WeaponType ), weaponType ) ||
             weaponType == WeaponType.None )
        {
            Console.WriteLine( "Неверный выбор оружия бойца" );
            Console.WriteLine();
            return WeaponType.None;
        }

        return weaponType;
    }

    private ArmorType GetArmorType()
    {
        Console.WriteLine( "Выберите броню бойца" );
        PrintMenu( MenuConstants.ChooseArmorOptions );
        string option = Console.ReadLine();

        if ( !Enum.TryParse<ArmorType>( option, out ArmorType armorType ) ||
             !Enum.IsDefined( typeof( ArmorType ), armorType ) ||
             armorType == ArmorType.None )
        {
            Console.WriteLine( "Неверный выбор брони бойца" );
            Console.WriteLine();
            return ArmorType.None;
        }

        return armorType;
    }

    private MenuOptionResult StartFight()
    {
        _arena.StartFight();
        return MenuOptionResult.Succses;
    }

    private MenuOptionResult Stop()
    {
        _isWorking = false;
        return MenuOptionResult.Succses;
    }
}