namespace Casino;

public class CasinoManager
{
    private double _balance = 0;

    public bool TryMakeDeposite()
    {
        Console.WriteLine( "Введите сумму пополнения баланса" );
        string depositStr = Console.ReadLine();
        if ( !double.TryParse( depositStr, out double deposit ) || deposit <= 0 )
        {
            return false;
        }

        if ( double.MaxValue - deposit < _balance )
        {
            return false;
        }

        _balance += deposit;

        return true;
    }

    public void ShowBalance()
    {
        Console.WriteLine( $"Текущий баланс {_balance}" );
    }

    public bool TryPlay()
    {
        Console.WriteLine( "Введите ставку" );
        string betStr = Console.ReadLine();

        if ( !double.TryParse( betStr, out double bet ) || bet >= _balance || bet <= 0 )
        {
            return false;
        }

        int seed = Random.Shared.Next( 1, 21 );
        if ( seed >= 18 && seed <= 20 )
        {
            double winAmount = CalculateWinAmount( bet, seed );
            _balance += winAmount;

            Console.WriteLine( $"Поздравляю, вы выиграли: {winAmount}, текущий баланс: {_balance}" );
            Console.WriteLine();
        }
        else
        {
            _balance -= bet;
            Console.WriteLine( $"К сожалению вы проиграли, текущий баланс {_balance}" );
        }

        return true;
    }

    private double CalculateWinAmount( double bet, int seed )
    {
        const int multiplicator = 25;
        double winMultiplicator = 1 + ( multiplicator * ( seed % 17 ) );

        return bet * winMultiplicator;
    }
}
