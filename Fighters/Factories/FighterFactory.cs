using Fighters.Factories.Enums;
using Fighters.Models.Fighters;
using Fighters.Models.Races;

namespace Fighters.Factories;

public static class FighterFactory
{
    public static IFighter Create( FighterType type, string name, IRace race )
    {
        switch ( type )
        {
            case FighterType.Knight:
                return new Knight( name, race );
            case FighterType.Barbarian:
                return new Barbarian( name, race );
            case FighterType.Assasin:
                return new Assasin( name, race );
            default:
                throw new ArgumentException( $"Неизвестный тип бойца: {type}" );
        }
    }
}