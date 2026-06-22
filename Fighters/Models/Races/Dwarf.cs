namespace Fighters.Models.Races;

public class Dwarf : IRace
{
    public int Damage => 2;

    public int Health => 120;

    public int Armor => 1;

    public int Initiative => 2;
}