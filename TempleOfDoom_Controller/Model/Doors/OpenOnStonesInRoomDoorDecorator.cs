using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_Game.Model.Doors;
public class OpenOnStonesInRoomDoorDecorator : BaseDoorDecorator
{
    private int noOfStones;

    public OpenOnStonesInRoomDoorDecorator(IDoor decoratedDoor, int noOfStones) : base(decoratedDoor)
    {
        this.noOfStones = noOfStones;
    }

    public override bool CanPass(Player player)
    {
        bool equalStones = (player.CurrentRoom.Items.Count(item => item is SankaraStone) == noOfStones);
        return equalStones && base.CanPass(player);
    }
}
