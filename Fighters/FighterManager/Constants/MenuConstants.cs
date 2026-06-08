namespace Fighters.Core.Constants;

public static class MenuConstants
{
    public static readonly List<string> MainMenuOptions = new()
    {
        "1 Добавить бойца",
        "2 Начать битву",
        "3 Выход"
    };

    public static readonly List<string> ChooseClassOptions = new()
    {
        "1 Рыцарь",
        "2 Варвар",
        "3 Убийца"
    };

    public static readonly List<string> ChooseRaceOptions = new()
    {
        "1 Человек",
        "2 Эльф",
        "3 Дворф",
        "4 Орк"
    };

    public static readonly List<string> ChooseWeaponOptions = new()
    {
        "1 Кулаки",
        "2 Копье",
        "3 Меч",
        "4 Длинный меч",
        "5 Молот"
    };

    public static readonly List<string> ChooseArmorOptions = new()
    {
        "1 Нет брони",
        "2 Лёгкая броня",
        "3 Тяжёлая броня",
        "4 Полный доспех"
    };
}