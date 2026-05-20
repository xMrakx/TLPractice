using OrderManager.Manager.Enums;

namespace OrderManager.Manager;

public class OrderManager
{
    private string _productName;
    private int _productCount;
    private string _userName;
    private string _address;
    private bool _hasData = false;

    private static List<string> _acceptMenuOptions =
    [
        "1 Подтвердить",
        "2 Отклонить"
    ];

    public bool TryRequestOrderDataFromUser()
    {
        Console.WriteLine( "Введите название товара" );
        _productName = Console.ReadLine();
        if ( string.IsNullOrEmpty( _productName ) )
        {
            return false;
        }

        Console.WriteLine();

        Console.WriteLine( "Введите количество товара" );
        string productCountStr = Console.ReadLine();
        if ( !int.TryParse( productCountStr, out _productCount ) || _productCount <= 0 )
        {
            return false;
        }

        Console.WriteLine();

        Console.WriteLine( "Введите ваше имя" );
        _userName = Console.ReadLine();
        if ( string.IsNullOrEmpty( _userName ) )
        {
            return false;
        }

        Console.WriteLine();

        Console.WriteLine( "Введите адрес" );
        _address = Console.ReadLine();
        if ( string.IsNullOrEmpty( _address ) )
        {
            return false;
        }

        _hasData = true;

        return true;
    }

    public bool IsValidOrderData()
    {
        if ( !_hasData )
        {
            Console.WriteLine( "Отсутстствуют данные заказа" );
            return false;
        }

        Console.WriteLine( $"Здравствуйте, {_userName}, вы заказали {_productCount} {_productName} на адрес {_address}, все верно?" );
        Console.WriteLine();

        return AwaitUserAccept();
    }

    private bool AwaitUserAccept()
    {
        while ( true )
        {
            PrintMenu( _acceptMenuOptions );
            string option = Console.ReadLine();
            OrderAcceptResult result = HandleOrderAcceptResult( option );

            switch ( result )
            {
                case OrderAcceptResult.Accept:
                    return true;
                case OrderAcceptResult.Decline:
                    ClearData();
                    return false;
                default:
                    Console.WriteLine( "Неверная команда" );
                    break;
            }
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

    private OrderAcceptResult HandleOrderAcceptResult( string option )
    {
        if ( !Enum.TryParse( option, out OrderAcceptOption acceptOption ) )
        {
            return OrderAcceptResult.InvalidOption;
        }

        switch ( acceptOption )
        {
            case OrderAcceptOption.Accept:
                return OrderAcceptResult.Accept;
            case OrderAcceptOption.Decline:
                return OrderAcceptResult.Decline;
            default:
                return OrderAcceptResult.InvalidOption;
        }
    }

    private void ClearData()
    {
        _productName = null;
        _productCount = 0;
        _userName = null;
        _address = null;
        _hasData = false;
    }   
}
