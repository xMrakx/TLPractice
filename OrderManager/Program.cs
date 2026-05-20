using OrderManager.Enums;

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
            return OrderHandleResult.InvalidOtion;
        }

        switch ( menuOption )
        {
            case MenuOption.MakeOrder:
                return CreateOrder();
            case MenuOption.Exit:
                return Exit();
            default:
                return OrderHandleResult.InvalidOtion;
        }
    }

    private static OrderHandleResult CreateOrder()
    {
        Manager.OrderManager orderManager = new Manager.OrderManager();

        if ( !orderManager.TryRequestOrderDataFromUser() )
        {
            return OrderHandleResult.InvalidOtion;
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
            case OrderHandleResult.InvalidOtion:
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




