using TempleOfDoom_DTO.DTO;
using TempleOfDoom_DTO.DTOFactory;
using TempleOfDoom_Game.Model;

namespace TempleOfDoom.Tests.Dto;

public class ConnectionDTOFactoryTests
{
    [Fact]
    public void CreateConnections_ResolvesRoomsByRoomIdInsteadOfListPosition()
    {
        Room room10 = CreateRoom(10);
        Room room20 = CreateRoom(20);
        ConnectionDTO dto = new()
        {
            North = 10,
            South = 20,
        };
        ConnectionDTOFactory factory = new([room10, room20], new DoorDTOFactory());

        List<Connection> connections = factory.CreateConnections([dto]);

        Connection connection = Assert.Single(connections);
        Assert.Contains(room10, connection.RoomDirectionMap.Keys);
        Assert.Contains(room20, connection.RoomDirectionMap.Keys);
    }

    [Fact]
    public void CreateConnections_ThrowsWhenConnectionReferencesUnknownRoomId()
    {
        ConnectionDTO dto = new()
        {
            North = 1,
            South = 99,
        };
        ConnectionDTOFactory factory = new([CreateRoom(1)], new DoorDTOFactory());

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => factory.CreateConnections([dto]));
        Assert.Contains("99", exception.Message);
    }

    private static Room CreateRoom(int id)
    {
        return new Room(id, 5, 5, "room", [], [], []);
    }
}
