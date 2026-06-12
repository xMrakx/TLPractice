namespace CarFactory.Models.CarBodies;

public class SUV : ICarBody
{
    public string TypeName => "внедорожник";

    public int SpeedModifier => 20;
}
