using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Items;
using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_DTO.DTOFactory;

public class ItemsDTOFactory
{
    /// <summary>
    ///  Maakt IItem objecten op basis van ItemDTO[].
    /// </summary>
    public List<IItem> CreateItems(ItemDTO[] itemsDTO)
    {
        List<IItem> itemList = [];
        foreach (ItemDTO itemDTO in itemsDTO)
        {
            IItem? newItem = CreateItemFromDTO(itemDTO);
            if (newItem != null)
            {
                itemList.Add(newItem);
            }
        }

        return itemList;
    }

    /// <summary>
    ///  Bouwt één IItem op basis van een ItemDTO.
    /// </summary>
    private IItem? CreateItemFromDTO(ItemDTO itemDTO)
    {
        Coordinates coordinates = new Coordinates(itemDTO.X, itemDTO.Y);

        switch (itemDTO.Type.ToLowerInvariant())
        {
            case "disappearing boobytrap":
                return new DisappearingBoobyTrap(coordinates, itemDTO.Damage);

            case "sankara stone":
                return new SankaraStone(coordinates);

            case "boobytrap":
                return new BoobyTrap(coordinates, itemDTO.Damage);

            case "key":
                return new Key(coordinates, itemDTO.Color ?? throw new InvalidOperationException("Een key item moet een color hebben."));

            case "pressure plate":
                return new PressurePlate(coordinates);

            default:
                return null;
        }
    }
}
