namespace TempleOfDoom_Game.Model.Items;
public class BoobyTrap : IItem
{
    public Coordinates Coordinates { get; set; }
    public int Damage { get; } 

    public BoobyTrap(Coordinates coordinates, int damage)
    {
        Coordinates = coordinates;
        Damage = damage;
    }

    public virtual void Interact(Player player) => player.RemoveLives(Damage);
}
