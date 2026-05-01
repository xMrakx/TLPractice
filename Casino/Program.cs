double balance = 0;
bool play = true;
const string header = "CASINO";
List<string> menuOptions = [
        "1. Пополнить баланс",
        "2. Показать баланс",
        "3. Играть",
        "4. Выйти"
        ];

PrintHeader();

while (play)
{
    PrintMenu();

    string option = Console.ReadLine();
    OptionHandleResult result = HandleOption(option);
    Console.WriteLine();
    Console.WriteLine(HandleResult(result));
    Console.WriteLine();
}

void PrintHeader()
{
    Console.WriteLine(header);
}

void PrintMenu()
{
    foreach (string option in menuOptions)
    {
        Console.WriteLine(option);
    }
    Console.WriteLine();
}

OptionHandleResult HandleOption(string option)
{
    switch (option)
    {
        case "1":
            return MakeDeposit();
        case "2":
            return ShowBalance();
        case "3":
            return Play();
        case "4":
            return Exit();
        default:
            return OptionHandleResult.InvalidOption;
    }
}

OptionHandleResult MakeDeposit()
{
    Console.WriteLine("Введите сумму пополнения баланса");
    string depositStr = Console.ReadLine();
    if (!double.TryParse(depositStr, out double deposit) || deposit <= 0)
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    if (double.MaxValue - deposit < balance)
    {
        return OptionHandleResult.InvalidDepositValue;
    }

    balance += deposit;
    return OptionHandleResult.Success;
}

OptionHandleResult ShowBalance()
{
    Console.WriteLine();
    Console.WriteLine($"Текущий баланс {balance}");
    return OptionHandleResult.Success;
}

OptionHandleResult Play()
{
    Console.WriteLine("Введите ставку");
    string betStr = Console.ReadLine();
    if (!double.TryParse(betStr, out double bet) || bet >= balance || bet <= 0)
    {
        return OptionHandleResult.InvalidBetValue;
    }

    int seed = Random.Shared.Next(1, 21);
    if (seed >= 18 && seed <= 20)
    {
        double winAmount = CalculateWinAmount(bet, seed);
        balance += winAmount;
        Console.WriteLine($"Поздравляю, вы выиграли: {winAmount}, текущий баланс: {balance}");
        Console.WriteLine();
    }
    else
    {
        balance -= bet;
        Console.WriteLine($"К сожалению вы проиграли, текущий баланс {balance}");
    }
    return OptionHandleResult.Success;
}

double CalculateWinAmount(double bet, int seed)
{
    const int multiplicator = 25;
    double winMultiplicator = 1 + (multiplicator * (seed % 17));

    return bet * winMultiplicator;
}

OptionHandleResult Exit()
{
    play = false;
    return OptionHandleResult.Success;
}

string HandleResult(OptionHandleResult result)
{
    switch (result)
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

enum OptionHandleResult
{
    Success = 0,
    InvalidOption = 1,

    InvalidDepositValue = 2,
    InvalidBetValue = 3
}