namespace Fighters.Models.Weapons;

public class Longsword : IWeapon
{
    public int Damage => 2;

    public int InitiativeModifier => 1;

    public string InstrumentalCaseName => "длинным мечом";
}