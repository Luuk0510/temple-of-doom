using TempleOfDoom_Game.Model;

namespace TempleOfDoom_DTO.DTOFactory;
public class PlayerDTOFactory
{
    private readonly Room[] _rooms;

    public PlayerDTOFactory(Room[] rooms) => _rooms = rooms;

    public Player CreatePlayer(DTO.PlayerDTO player)
    {
        int startRoomId = player.StartRoomId - 1;
        Coordinates startCoordinates = new Coordinates(player.StartX, player.StartY);

        return new Player(_rooms[startRoomId], startCoordinates, player.Lives);
    }
}
