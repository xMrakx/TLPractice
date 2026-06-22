using Fighters.Factories.Enums;
using Fighters.Models.Weapons;

namespace Fighters.Factories;

public static class WeaponFactory
{
    public static IWeapon Create( WeaponType type )
    {
        switch ( type )
        {
            case WeaponType.Fists:
                return new Fists();
            case WeaponType.Spear:
                return new Spear();
            case WeaponType.Sword:
                return new Sword();
            case WeaponType.Longsword:
                return new Longsword();
            case WeaponType.Hammer:
                return new Hammer();
            default:
                throw new ArgumentException( $"Неизвестный тип оружия: {type}" );
        }
    }
}