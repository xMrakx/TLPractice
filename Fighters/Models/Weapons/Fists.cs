namespace Fighters.Models.Weapons;

public class Fists : IWeapon
{
    public int Damage => 1;

    public int InitiativeModifier => 0;

    public string InstrumentalCaseName => "кулаками";
}