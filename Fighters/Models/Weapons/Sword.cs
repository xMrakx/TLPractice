namespace Fighters.Models.Weapons;

public class Sword : IWeapon
{
    public int Damage => 2;

    public int InitiativeModifier => 0;

    public string InstrumentalCaseName => "мечом";
}
