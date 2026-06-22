using Casino.Enums;

namespace Casino;

public class Program
{
    private static bool _isWorking = true;
    private static string _header = "CASINO";
    private static List<string> _menuOptions =
    [
        "1. Пополнить баланс",
        "2. Показать баланс",
        "3. Играть",
        "4. Выйти"
    ];

    private readonly CasinoManager _manager;

    public Program()
    {
        _manager = new CasinoManager();
    }

    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    private void Run()
    {
        PrintHeader();
        while ( _isWorking )
        {
            PrintMenu();
            string option = Console.ReadLine();
            OptionHandleResult result = HandleOption( option );
            string handledResult = HandleResult( result );
            Console.WriteLine( handledResult );
        }
    }

    private static void PrintHeader()
    {
        Console.WriteLine( _header );
    }

    private static void PrintMenu()
    {
        foreach ( string option in _menuOptions )
        {
            Console.WriteLine( option );
        }
        Console.WriteLine();
    }

    private static OptionHandleResult HandleOption( string option )
    {
        if ( !Enum.TryParse( option, out MenuOption menuOption ) )
        {
            return OptionHandleResult.InvalidOption;
        }

        switch ( menuOption )
        {
            case MenuOption.MakeDeposit:
                return MakeDeposit();
            case MenuOption.ShowBalance:
                return ShowBalance();
            case MenuOption.Play:
                return Play();
            case MenuOption.Exit:
                return Exit();
            default:
                return OptionHandleResult.InvalidOption;
        }
    }

    private static OptionHandleResult MakeDeposit()
    {
        if ( !_manager.TryMakeDeposite() )
        {
            return OptionHandleResult.InvalidDepositValue;
        }

        return OptionHandleResult.Success;
    }

    private static OptionHandleResult ShowBalance()
    {
        _manager.ShowBalance();

        return OptionHandleResult.Success;
    }

    private static OptionHandleResult Play()
    {
        if ( !_manager.TryPlay() )
        {
            return OptionHandleResult.InvalidBetValue;
        }

        return OptionHandleResult.Success;
    }

    private static OptionHandleResult Exit()
    {
        _isWorking = false;

        return OptionHandleResult.Success;
    }

    private static string HandleResult( OptionHandleResult result )
    {
        switch ( result )
        {
            case OptionHandleResult.Success:
                return "CASINO";
            case OptionHandleResult.InvalidOption:
                return "Неверная команда";
            case OptionHandleResult.InvalidDepositValue:
                return "Неверная сумма пополнения баланса";
            case OptionHandleResult.InvalidBetValue:
                return "Неверная ставка";
            default:
                return "Критическая ошибка";
        }
    }
}
