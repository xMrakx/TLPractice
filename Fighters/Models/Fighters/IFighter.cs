using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }

    public string GetClassName();
    public IWeapon GetWeapon();

    public int GetCurrentHealth();
    public int GetMaxHealth();
    public int CalculateDamage();
    public int CalculateArmor();
    public int CalculateInitiative();

    public void SetArmor(IArmor armor);
    public void SetWeapon(IWeapon weapon);

    public void TakeDamage(int damage);

    public void RestoreHealth();
}