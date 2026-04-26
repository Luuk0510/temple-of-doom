using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom.Tests.Model;

public class GameApplicationTests
{
    [Fact]
    public void CheckWinCondition_ReturnsTrueWhenPlayerHasFiveSankaraStones()
    {
        Room room = CreateRoom();
        Player player = new(room, new Coordinates(2, 2), 3);
        GameApplication game = new([room], player, []);

        for (int i = 0; i < 5; i++)
        {
            player.AddItem(new SankaraStone(new Coordinates(i, 1)));
        }

        Assert.True(game.CheckWinCondition());
    }

    [Fact]
    public void CheckLoseCondition_ReturnsTrueWhenPlayerHasNoLivesLeft()
    {
        Room room = CreateRoom();
        Player player = new(room, new Coordinates(2, 2), 1);
        GameApplication game = new([room], player, []);

        player.RemoveLives(1);

        Assert.True(game.CheckLoseCondition());
    }

    private static Room CreateRoom()
    {
        return new Room(1, 5, 5, "room", [], [], []);
    }
}
