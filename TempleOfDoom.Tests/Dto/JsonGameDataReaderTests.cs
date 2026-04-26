using TempleOfDoom_DTO;

namespace TempleOfDoom.Tests.Dto;

public class JsonGameDataReaderTests
{
    [Fact]
    public void ReadFile_MapsJsonNamesToPascalCasePropertiesAndUsesEmptyDefaults()
    {
        string json = """
        {
          "rooms": [
            {
              "id": 1,
              "type": "room",
              "width": 5,
              "height": 5
            }
          ],
          "connections": [
            {
              "NORTH": 1,
              "SOUTH": 1
            }
          ],
          "player": {
            "startRoomId": 1,
            "startX": 2,
            "startY": 2,
            "lives": 3
          }
        }
        """;

        string filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.json");
        File.WriteAllText(filePath, json);

        try
        {
            JsonFileReaderStrategy reader = new JsonFileReaderStrategy();

            var game = reader.ReadFile(filePath);

            Assert.Single(game.Rooms);
            Assert.Equal("room", game.Rooms[0].Type);
            Assert.Empty(game.Rooms[0].Items);
            Assert.Empty(game.Rooms[0].Enemies);
            Assert.Empty(game.Rooms[0].SpecialFloorTiles);
            Assert.Single(game.Connections);
            Assert.Empty(game.Connections[0].Doors);
            Assert.Equal(1, game.Player.StartRoomId);
            Assert.Equal(3, game.Player.Lives);
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}
