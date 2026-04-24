namespace TempleOfDoom_Game.Model.Doors;
public class OpenOnOddDoorDecorator : BaseDoorDecorator
{
    public OpenOnOddDoorDecorator(IDoor decoratedDoor) : base(decoratedDoor){}

    /// <summary>
    ///  Controleert of het aantal levens van de speler oneven is
    ///  </summary>
    public override bool CanPass(Player player)
    {
        bool IsOdd = player.Lives % 2 != 0;
        return IsOdd && base.CanPass(player);
    }
}
