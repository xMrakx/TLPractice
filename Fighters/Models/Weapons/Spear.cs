namespace Fighters.Models.Weapons;

public class Spear : IWeapon
{
    public int Damage => 1;

    public int InitiativeModifier => 2;

    public string InstrumentalCaseName => "копьем";
}