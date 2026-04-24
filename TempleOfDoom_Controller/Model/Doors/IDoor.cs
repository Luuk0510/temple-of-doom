namespace TempleOfDoom_Game.Model.Doors;
public interface IDoor
{
    bool IsOpen { get; set; }
    string? Color { get; set; }

    bool CanPass(Player player);
}
