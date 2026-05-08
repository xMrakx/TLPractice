namespace Fighters.Models.Races;

public class Elf : IRace
{
    public int Damage => 1;

    public int Health => 100;

    public int Armor => 0;

    public int Initiative => 4;
}