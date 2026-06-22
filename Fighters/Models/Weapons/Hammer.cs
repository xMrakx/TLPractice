namespace Fighters.Models.Weapons;

public class Hammer : IWeapon
{
    public int Damage => 3;

    public int InitiativeModifier => -2;

    public string InstrumentalCaseName => "молотом";
}