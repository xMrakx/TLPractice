bool work = true;
List<string> menuOptions = [
    "1 Калькулятор",
    "2 Выход"
    ];

while (work)
{
    PrintMenu();
    string option = Console.ReadLine();
    OptionalHandleResult result = HandleOption(option);
    Console.WriteLine(HandleResult(result));
}

void PrintMenu()
{
    foreach (string option in menuOptions)
    {
        Console.WriteLine(option);
    }
    Console.WriteLine();
}

OptionalHandleResult HandleOption(string option)
{
    switch (option)
    {
        case "1":
            return Calculate();
        case "2":
            return Exit();
        default:
            return OptionalHandleResult.InvalidOption;
    }
}

OptionalHandleResult Calculate()
{
    Console.WriteLine("Введите выражение: {число} действие {число}.");
    Console.WriteLine("Доступные действия: +, -, *, /");
    Console.WriteLine();

    int firstOperand;
    int secondOperand;
    Action action;
    try
    {
        string[] arr = ReadInput();
        firstOperand = ParseOperand(arr[0]);
        action = ParseAction(arr[1]);
        secondOperand = ParseOperand(arr[2]);

        if (secondOperand == 0 && action == Action.Divide)
            return OptionalHandleResult.InvalidInput;
    }
    catch (Exception ex)
    {
        return OptionalHandleResult.InvalidInput;
    }

    try
    {
        int result = HandleAction(firstOperand, action, secondOperand);
        Console.WriteLine($"Результат: {result}");
        Console.WriteLine();
    }
    catch
    {
        return OptionalHandleResult.Overflow;
    }
    return OptionalHandleResult.Success;
}

string[] ReadInput()
{
    string input = Console.ReadLine();
    string[] inputArr = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    if (inputArr.Length != 3)
    {
        throw new Exception();
    }
    return inputArr;
}

int ParseOperand(string input)
{
    if (!int.TryParse(input, out int intOperand))
        throw new Exception();
    return intOperand;
}

Action ParseAction(string input)
{
    switch (input)
    {
        case "+":
            return Action.Add;
        case "-":
            return Action.Subtract;
        case "*":
            return Action.Multiply;
        case "/":
            return Action.Divide;
        default:
            throw new Exception();
    }
}

int HandleAction(int firstOperand, Action action, int secondOperand)
{
    switch (action)
    {
        case Action.Add:
            return firstOperand + secondOperand;
        case Action.Subtract:
            return firstOperand - secondOperand;
        case Action.Multiply:
            return firstOperand * secondOperand;
        case Action.Divide:
            return firstOperand / secondOperand;
        default:
            throw new Exception();
    }
}

OptionalHandleResult Exit()
{
    work = false;
    return OptionalHandleResult.Success;
}

string HandleResult(OptionalHandleResult result)
{
    switch (result)
    {
        case OptionalHandleResult.Success:
            return "";
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

enum OptionalHandleResult
{
    Success = 0,
    InvalidOption = 1,
    InvalidInput = 2,
    Overflow = 3
}

enum Action
{
    Add,
    Subtract,
    Multiply,
    Divide
}