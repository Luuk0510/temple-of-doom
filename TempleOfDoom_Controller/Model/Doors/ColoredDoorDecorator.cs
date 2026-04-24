using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_Game.Model.Doors;
public class ColoredDoorDecorator : BaseDoorDecorator
{
    public ColoredDoorDecorator(IDoor decoratedDoor, string color) : base(decoratedDoor)
    {
        Color = color;
    }


    /// <summary>
    ///  Controleert of de speler een sleutel heeft met dezelfde kleur als deze deur.
    /// </summary>
    public override bool CanPass(Player player)
    {
        bool hasMatchingKey = player.Inventory
                                .OfType<Key>()
                                .Any(key => string.Equals(key.Color, Color, StringComparison.OrdinalIgnoreCase));

        return base.CanPass(player) && hasMatchingKey;

    }
}
