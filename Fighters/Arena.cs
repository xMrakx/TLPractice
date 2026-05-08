using Fighters.Extensions;
using Fighters.Models.Fighters;

namespace Fighters;

public class Arena
{
    private class FightersPair
    {
        public IFighter Fighter1 { get; set; }
        public IFighter Fighter2 { get; set; }
    }

    private List<FightersPair> _fightersPairs = new();
    private List<IFighter> _initiativeQueue = new();
    private List<IFighter> _waitingFighters = new();
    private IFighter _unpairFighter;

    public List<IFighter> Fighters { get; }

    public Arena()
    {
        Fighters = new List<IFighter>();
    }

    public void AddFighter( IFighter fighter )
    {
        Fighters.Add( fighter );
    }

    public void StartFight()
    {
        if ( Fighters.Count < 2 )
        {
            Console.WriteLine();
            Console.WriteLine( "Недостаточно бойцов для начала боя." );
            return;
        }

        GeneratePairs();
        DisplayPairs();
        Fight();
        RestoreFightersHealth();
    }

    private void GeneratePairs()
    {
        List<IFighter> shuffledFighters = GetShuffledFighterList();

        _fightersPairs.Clear();
        _unpairFighter = null;

        for ( int i = 0; i < shuffledFighters.Count; i += 2 )
        {
            if ( i + 1 < shuffledFighters.Count )
            {
                _fightersPairs.Add( new FightersPair() { Fighter1 = shuffledFighters[ i ], Fighter2 = shuffledFighters[ i + 1 ] } );
            }
            else
            {
                _unpairFighter = shuffledFighters[ i ];
            }
        }
    }

    private List<IFighter> GetShuffledFighterList()
    {
        Random random = new Random();
        return Fighters.OrderBy( f => random.Next() ).ToList();
    }

    private void DisplayPairs()
    {
        Console.WriteLine( "Паринги" );
        foreach ( var pair in _fightersPairs )
        {
            Console.WriteLine(
                $"{pair.Fighter1.GetClassName()} {pair.Fighter1.Name} " +
                $"VS {pair.Fighter2.GetClassName()} {pair.Fighter2.Name}" );
        }
        if ( _unpairFighter != null )
        {
            Console.WriteLine( $"Ожидающи очереди: {_unpairFighter.GetClassName()} {_unpairFighter.Name}" );
        }
    }

    private void Fight()
    {
        Console.WriteLine();
        Console.WriteLine( "Бой начался!" );
        _initiativeQueue.Clear();

        // Перемешиваем перед сортировкой
        // чтобы бойцы с одинаковой инициативой попадали в очередь в случайном порядке
        _initiativeQueue = GetShuffledFighterList().OrderByDescending( f => f.CalculateInitiative() ).ToList();
        if ( _unpairFighter != null )
        {
            _initiativeQueue.Remove( _unpairFighter );
        }

        int roundCounter = 0;
        while ( _initiativeQueue.Count > 1 )
        {
            roundCounter++;
            Console.WriteLine( $"Раунд {roundCounter}" );

            for ( int i = 0; i < _initiativeQueue.Count; i++ )
            {
                IFighter fighter = _initiativeQueue[ i ];
                FightersPair pair = GetPair( fighter );
                IFighter target = pair.Fighter1 == fighter ? pair.Fighter2 : pair.Fighter1;
                Strike( _initiativeQueue[ i ], target );

                if ( !target.IsAlive() )
                {
                    Console.WriteLine( $"{target.GetClassName()} {target.Name} погибает" );
                    ActualizePairsAfterOneFighterDie( fighter, target, pair );
                }
            }

            if ( _waitingFighters.Count > 0 )
            {
                _initiativeQueue.AddRange( _waitingFighters );
                _waitingFighters.Clear();
                _initiativeQueue = _initiativeQueue.OrderByDescending( f => f.CalculateInitiative() ).ToList();
            }
        }

        IFighter winner = _unpairFighter;
        Console.WriteLine( $"{winner.GetClassName()} {winner.Name} ПОБЕЖДАЕТ" );
    }

    private FightersPair GetPair( IFighter fighter )
    {
        return _fightersPairs.FirstOrDefault( p => p.Fighter1 == fighter || p.Fighter2 == fighter );
    }

    private void Strike( IFighter fighter, IFighter target )
    {
        Random random = new Random();
        float atackModifier = ( float )random.Next( -20, 11 ) / 100;
        if ( atackModifier <= -0.19f )
        {
            Console.WriteLine(
                $"{fighter.GetClassName()} {fighter.Name} " +
                $"ПРОМАХИВАЕТСЯ по {target.GetClassName()} {target.Name}" );

            return;
        }

        int damage = fighter.CalculateDamage();
        if ( atackModifier >= 0.09f )
        {
            damage *= 2;
        }
        else
        {
            damage += ( int )( damage * atackModifier );
        }
        int realDamage = damage - target.CalculateArmor();
        if ( realDamage < 0 )
        {
            realDamage = 0;
        }
        target.TakeDamage( realDamage );

        Console.WriteLine(
            $"{fighter.GetClassName()} {fighter.Name} " +
            $"бьет {fighter.GetWeapon().InstrumentalCaseName} " +
            $"по {target.GetClassName()} {target.Name} " +
            $"нанося {realDamage} урона" );
    }

    private void ActualizePairsAfterOneFighterDie( IFighter fighter, IFighter target, FightersPair pair )
    {
        _initiativeQueue.Remove( target );
        if ( _unpairFighter != null )
        {
            if ( pair.Fighter1 == target )
            {
                pair.Fighter1 = _unpairFighter;
            }
            else
            {
                pair.Fighter2 = _unpairFighter;
            }
            _waitingFighters.Add( _unpairFighter );
            _unpairFighter = null;
        }
        else
        {
            _unpairFighter = fighter;
            _initiativeQueue.Remove( fighter );
            _fightersPairs.Remove( pair );
        }
    }

    private void RestoreFightersHealth()
    {
        foreach ( IFighter fighter in Fighters )
        {
            fighter.RestoreHealth();
        }
    }
}