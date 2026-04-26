using TempleOfDoom_DTO.DTO;
using TempleOfDoom_DTO.DTOFactory;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Doors;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom.Tests.Dto;

public class DoorDTOFactoryTests
{
    [Fact]
    public void InitializeDoors_MatchesDoorTypeCaseInsensitively()
    {
        DoorDTO dto = new()
        {
            Type = "CoLoReD",
            Color = "green",
        };
        IDoor door = new DoorDTOFactory().InitializeDoors([dto], [])!;
        Room room = new(1, 5, 5, "room", [], [], []);
        Player player = new(room, new Coordinates(2, 2), 3);
        player.AddItem(new Key(new Coordinates(1, 1), "GREEN"));

        Assert.True(door.CanPass(player));
    }

    [Fact]
    public void InitializeDoors_ThrowsWhenColoredDoorHasNoColor()
    {
        DoorDTO dto = new()
        {
            Type = "colored",
        };

        Assert.Throws<InvalidOperationException>(() => new DoorDTOFactory().InitializeDoors([dto], []));
    }
}
