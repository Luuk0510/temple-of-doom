namespace TempleOfDoom_Game.Model.Items;
public interface IItem
{
    Coordinates Coordinates { get; set; }

    void Interact(Player player);
}
