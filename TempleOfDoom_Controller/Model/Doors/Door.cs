namespace TempleOfDoom_Game.Model.Doors;
public class Door : IDoor
{
    public bool IsOpen { get; set; }
    public string? Color { get; set; }

    public Door() => IsOpen = true;

    public bool CanPass(Player player) => IsOpen;
}
