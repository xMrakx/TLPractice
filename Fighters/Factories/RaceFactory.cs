using Fighters.Factories.Enums;
using Fighters.Models.Races;

namespace Fighters.Factories;

public static class RaceFactory
{
    public static IRace Create( RaceType type )
    {
        switch ( type )
        {
            case RaceType.Human:
                return new Human();
            case RaceType.Elf:
                return new Elf();
            case RaceType.Dwarf:
                return new Dwarf();
            case RaceType.Orc: 
                return new Orc();
            default:
                throw new ArgumentException( $"Неизвестный вид рассы {type}" );
        }
    }
}