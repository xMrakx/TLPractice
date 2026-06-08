using Fighters.Core.Constants;
using Fighters.Core.Enums;
using Fighters.Factories;
using Fighters.Factories.Enums;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.FighterManager;

public class FighterManager
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
        IFighter fighter = FighterBuilder.Build();

        _arena.AddFighter( fighter );

        return MenuOptionResult.Succses;
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