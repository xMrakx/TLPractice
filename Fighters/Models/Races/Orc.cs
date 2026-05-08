namespace Fighters.Models.Races;

public class Orc : IRace
{
    public int Damage => 3;

    public int Health => 120;

    public int Armor => 0;

    public int Initiative => 3;
}