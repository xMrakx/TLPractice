using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Assasin : IFighter
{
    private readonly IRace _race;
    private IArmor _armor = new NoArmor();
    private IWeapon _weapon = new Fists();
    private int _classHealth = 0;
    private int _classDamage = 3;
    private int _classArmor = 0;
    private int _classInitiative = 3;

    private int _currentHealth;

    public string Name { get; private set; }

    public Assasin( string name, IRace race )
    {
        Name = name;
        _race = race;

        _currentHealth = GetMaxHealth();
    }

    public string GetClassName() => "Убийца";

    public IWeapon GetWeapon() => _weapon;

    public int GetCurrentHealth() => _currentHealth;

    public int GetMaxHealth() => _race.Health + _classHealth;

    public int CalculateDamage() => _weapon.Damage + _race.Damage + _classDamage;

    public int CalculateArmor() => _armor.Armor + _race.Armor + _classArmor;

    public int CalculateInitiative()
    {
        int initiative =
             _race.Initiative
             + _classInitiative
             + _armor.InitiativeModifier
             + _weapon.InitiativeModifier;

        if ( initiative <= 0 )
        {
            return 1;
        }

        return initiative;
    }

    public void SetArmor( IArmor armor )
    {
        _armor = armor;
    }

    public void SetWeapon( IWeapon weapon )
    {
        _weapon = weapon;
    }

    public void TakeDamage( int damage )
    {
        int newHealth = _currentHealth - damage;
        if ( newHealth < 0 )
        {
            newHealth = 0;
        }

        _currentHealth = newHealth;
    }

    public void RestoreHealth()
    {
        _currentHealth = GetMaxHealth();
    }
}