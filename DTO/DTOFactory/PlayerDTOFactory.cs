using TempleOfDoom_Game.Model;

namespace TempleOfDoom_DTO.DTOFactory;
public class PlayerDTOFactory
{
    private readonly Room[] _rooms;

    public PlayerDTOFactory(Room[] rooms) => _rooms = rooms;

    public Player CreatePlayer(DTO.PlayerDTO player)
    {
        int startRoomId = player.startRoomId - 1;
        Coordinates startCoordinates = new Coordinates(player.startX, player.startY);

        return new Player(_rooms[startRoomId], startCoordinates, player.lives);
    }
}
