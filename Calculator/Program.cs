using Calculator.Enums;

namespace Calculator;

public class Program
{
    private static bool _isWorking = true;
    private static List<string> _menuOptions =
    [
        "1 Калькулятор",
        "2 Выход"
    ];

    public static void Main( string[] args )
    {
        while ( _isWorking )
        {
            PrintMenu();
            string option = Console.ReadLine();
            OptionalHandleResult result = HandleOption( option );
            string handledResult = HandleResult( result );
            if ( handledResult != null )
            {
                Console.WriteLine( handledResult );
            }
        }
    }
    
    private static void PrintMenu()
    {
        foreach ( string option in _menuOptions )
        {
            Console.WriteLine( option );
        }
        Console.WriteLine();
    }

    private static OptionalHandleResult HandleOption( string option )
    {
        if ( !Enum.TryParse( option, out MenuOption menuOption ) )
        {
            return OptionalHandleResult.InvalidOption;
        }

        switch ( menuOption )
        {
            case MenuOption.Calculator:
                return Calculate();
            case MenuOption.Exit:
                return Exit();
            default:
                return OptionalHandleResult.InvalidOption;
        }
    }

    private static OptionalHandleResult Calculate()
    {
        Console.WriteLine( "Введите выражение: {число} действие {число}." );
        Console.WriteLine( "Доступные действия: +, -, *, /" );
        Console.WriteLine();

        int firstOperand = 0;
        int secondOperand = 0;
        MathAction action = MathAction.InvalidAction;

        Calculator calculator = new Calculator();

        string[] arr = ReadInput();

        if ( !calculator.TryParseInput( arr, out firstOperand, out action, out secondOperand ) )
        {
            return OptionalHandleResult.InvalidInput;
        }

        if ( calculator.IsOverflow( firstOperand, secondOperand ) )
        {
            return OptionalHandleResult.Overflow;
        }

        if ( !calculator.TryHandleAction( firstOperand, action, secondOperand, out int result ) )
        {
            return OptionalHandleResult.InvalidInput;
        }

        Console.WriteLine( $"Результат: {result}" );
        Console.WriteLine();

        return OptionalHandleResult.Success;
    }

    private static string[] ReadInput()
    {
        string input = Console.ReadLine();
        string[] inputArr = input.Split( " ", StringSplitOptions.RemoveEmptyEntries );

        return inputArr;
    }

    private static OptionalHandleResult Exit()
    {
        _isWorking = false;

        return OptionalHandleResult.Success;
    }

    private static string HandleResult( OptionalHandleResult result )
    {
        switch ( result )
        {
            case OptionalHandleResult.Success:
                return null;
            case OptionalHandleResult.InvalidOption:
                return "Неверная команда";
            case OptionalHandleResult.InvalidInput:
                return "Неверно введеные данные";
            case OptionalHandleResult.Overflow:
                return "Ошибка переполнения";
            default:
                return "Критическая ошибка";
        }
    }  
}
