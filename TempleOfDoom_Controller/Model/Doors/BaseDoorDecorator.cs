namespace TempleOfDoom_Game.Model.Doors;
public abstract class BaseDoorDecorator : IDoor
{
    private IDoor _decoratedDoor { get; }

    public BaseDoorDecorator(IDoor decoratedDoor)
    {
        _decoratedDoor = decoratedDoor;
    }

    public virtual bool IsOpen { get => _decoratedDoor.IsOpen; set => _decoratedDoor.IsOpen = value; }

    public virtual string Color { get => _decoratedDoor.Color; set => _decoratedDoor.Color = value; }

    public virtual bool CanPass(Player player) => _decoratedDoor.CanPass(player);
    
}
