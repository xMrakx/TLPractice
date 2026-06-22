using OrderManager.Enums;
using OM = OrderManager.Manager.OrderManager;

namespace OrderManager;

public class Program
{
    private static bool _isWorking = true;
    private static List<string> _menuOptions =
    [
        "1 Оформить заказ",
        "2 Выход"
    ];

    public static void Main( string[] args )
    {
        while ( _isWorking )
        {
            PrintMenu( _menuOptions );
            string option = Console.ReadLine();
            OrderHandleResult result = HandleOption( option );
            Console.WriteLine( HandleResult( result ) );
            Console.WriteLine();
        }
    }

    private static void PrintMenu( List<string> menu )
    {
        foreach ( string option in menu )
        {
            Console.WriteLine( option );
        }
        Console.WriteLine();
    }

    private static OrderHandleResult HandleOption( string option )
    {
        if ( !Enum.TryParse(option, out MenuOption menuOption) )
        {
            return OrderHandleResult.InvalidOption;
        }

        switch ( menuOption )
        {
            case MenuOption.MakeOrder:
                return CreateOrder();
            case MenuOption.Exit:
                return Exit();
            default:
                return OrderHandleResult.InvalidOption;
        }
    }

    private static OrderHandleResult CreateOrder()
    {
        OM orderManager = new();

        if ( !orderManager.TryRequestOrderDataFromUser() )
        {
            return OrderHandleResult.InvalidOption;
        }

        Console.WriteLine();

        if ( !orderManager.IsValidOrderData() )
        {
            return OrderHandleResult.OrderDecline;
        }

        return OrderHandleResult.Success;
    }

    private static OrderHandleResult Exit()
    {
        _isWorking = false;

        return OrderHandleResult.Exit;
    }

    private static string HandleResult( OrderHandleResult result )
    {
        switch ( result )
        {
            case OrderHandleResult.Success:
                return "Заказ создан";
            case OrderHandleResult.InvalidOption:
                return "Неверная команда";
            case OrderHandleResult.InvalidInput:
                return "Неверные данные";
            case OrderHandleResult.OrderDecline:
                return "Заказ отменен";
            case OrderHandleResult.Exit:
                return "";
            default:
                return "Критическая ошибка";
        }
    }
}
