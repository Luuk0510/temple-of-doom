namespace TempleOfDoom_Game.Model.Items;
public class DisappearingBoobyTrap : BoobyTrap
{
    public DisappearingBoobyTrap(Coordinates coordinates, int damage) : base(coordinates, damage) {}

    public override void Interact(Player player)
    {
        player.RemoveLives(Damage);
        player.CurrentRoom.RemoveItem(this);
    }
}
