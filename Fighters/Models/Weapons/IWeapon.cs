namespace Fighters.Models.Weapons;

public interface IWeapon
{
    public int Damage { get; }
    public int InitiativeModifier { get; }
    public string InstrumentalCaseName { get; }
}