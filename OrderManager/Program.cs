
bool work = true;
List<string> menuOptions = [
    "1 Оформить заказ",
    "2 Выход"
    ];
List<string> acceptMenuOptions = [
    "1 Подтвердить",
    "2 Отклонить"
    ];

while ( work )
{
    PrintMenu( menuOptions );
    string option = Console.ReadLine();
    OrderHandleResult result = HandleOption( option );
    Console.WriteLine( HandleResult( result ) );
    Console.WriteLine();
}

void PrintMenu( List<string> menu )
{
    foreach ( string option in menu )
    {
        Console.WriteLine( option );
    }
    Console.WriteLine();
}

OrderHandleResult HandleOption( string option )
{
    switch ( option )
    {
        case "1":
            return CreateOrder();
        case "2":
            return Exit();
        default:
            return OrderHandleResult.InvalidOtion;
    }
}

OrderHandleResult CreateOrder()
{
    Console.WriteLine( "Введите название товара" );
    string productName = Console.ReadLine();
    if ( string.IsNullOrEmpty( productName ) )
        return OrderHandleResult.InvalidInput;
    Console.WriteLine();

    Console.WriteLine( "Введите количество товара" );
    string countStr = Console.ReadLine();
    if ( !int.TryParse( countStr, out int count ) || count <= 0 )
        return OrderHandleResult.InvalidInput;
    Console.WriteLine();

    Console.WriteLine( "Введите ваше имя" );
    string userName = Console.ReadLine();
    if ( string.IsNullOrEmpty( userName ) )
        return OrderHandleResult.InvalidInput;
    Console.WriteLine();

    Console.WriteLine( "Введите адрес" );
    string adress = Console.ReadLine();
    if ( string.IsNullOrEmpty( adress ) )
        return OrderHandleResult.InvalidInput;
    Console.WriteLine();

    Console.WriteLine( $"Здравствуйте, {userName}, вы заказали {count} {productName} на адрес {adress}, все верно?" );
    Console.WriteLine();
    bool invalidInput = true;
    while ( invalidInput )
    {
        PrintMenu( acceptMenuOptions );
        string option = Console.ReadLine();
        OrderAcceptResult result = HandleOrderAcceptrResult( option );
        switch ( result )
        {
            case OrderAcceptResult.Accept:
                invalidInput = false;
                break;
            case OrderAcceptResult.Decline:
                return OrderHandleResult.OrderDecline;
            default:
                Console.WriteLine( "Неверная команда" );
                break;
        }

    }
    return OrderHandleResult.Success;
}

OrderAcceptResult HandleOrderAcceptrResult( string option )
{
    switch ( option )
    {
        case "1":
            return OrderAcceptResult.Accept;
        case "2":
            return OrderAcceptResult.Decline;
        default:
            return OrderAcceptResult.invalidOption;
    }

}

OrderHandleResult Exit()
{
    work = false;
    return OrderHandleResult.Exit;
}

string HandleResult( OrderHandleResult result )
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

enum OrderHandleResult
{
    Success = 0,
    InvalidOtion = 1,
    InvalidInput = 2,
    OrderDecline = 3,
    Exit = 4
}

enum OrderAcceptResult
{
    Accept = 0,
    Decline = 1,
    invalidOption = 2
}