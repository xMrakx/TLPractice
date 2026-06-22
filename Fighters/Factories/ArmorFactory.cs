using Fighters.Factories.Enums;
using Fighters.Models.Armors;

namespace Fighters.Factories;

public static class ArmorFactory
{
    public static IArmor Create( ArmorType type )
    {
        switch ( type )
        {
            case ArmorType.NoArmor:
                return new NoArmor();
            case ArmorType.Light:
                return new LightArmor();
            case ArmorType.Heavy:
                return new HeavyArmor();
            case ArmorType.FullPlate:
                return new FullPlateArmor();
            default:
                throw new ArgumentException( $"Неизвестный тип брони: {type}" );
        }
    }
}