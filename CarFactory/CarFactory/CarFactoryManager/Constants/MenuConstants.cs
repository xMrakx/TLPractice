namespace CarFactory.CarFactoryManager.Constants;

public static class MenuConstants
{
    public static readonly List<string> MainMenuOptions = new()
    {
        "1 Создать новую конфигурацию",
        "2 Вывести текущую конфигурацию",
        "3 Выход"
    };

    public static readonly List<string> ChooseCarBodyTypeOptions = new()
    {
        "1 Седан",
        "2 Хэтчбэк",
        "3 Внедорожник"
    };

    public static readonly List<string> ChooseCarColorOptions = new()
    {
        "1 Красный",
        "2 Зеленый",
        "3 Синий"
    };

    public static readonly List<string> ChooseEngineType = new()
    {
        "1 Бензиновый",
        "2 Дизельный",
        "3 Электрический"
    };

    public static readonly List<string> ChooseSteeringWheelType = new()
    {
        "1 Слева",
        "2 Справа"
    };

    public static readonly List<string> ChooseTransmissionType = new()
    {
        "1 Механическая",
        "2 Автоматическая"
    };
}
