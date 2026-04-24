using TempleOfDoom_DTO.DTO;
using TempleOfDoom_DTO.DTOFactory;
using TempleOfDoom_Game.Model;

namespace TempleOfDoom_DTO;
public class GameReader
{
    private readonly IFileReaderStrategy _fileReaderStrategy;

    public GameReader(IFileReaderStrategy fileReaderStrategy) => _fileReaderStrategy = fileReaderStrategy;

    /// <summary>
    ///  Leest het JSON bestand in en bouwt daaruit een GameApplication.
    /// </summary>
    public GameApplication ReadGame(string filePath)
    {
        // Lees het rootobject in
        GameDTO root = _fileReaderStrategy.ReadFile(filePath);

        // Maak de nodige factories
        ItemsDTOFactory itemsDTOFactory = new ItemsDTOFactory();
        DoorDTOFactory doorDTOFactory = new DoorDTOFactory();
        EnemyDTOFactory enemyDTOFactory = new EnemyDTOFactory();

        // Bouw Rooms
        RoomFactory roomFactory = new RoomFactory(itemsDTOFactory, enemyDTOFactory);
        List<Room> rooms = roomFactory.CreateRooms(root.rooms);

        // Bouw Connections
        ConnectionDTOFactory connectionDTOFactory = new ConnectionDTOFactory(rooms, doorDTOFactory);
        List<Connection> connections = connectionDTOFactory.CreateConnections(root.connections);

        // Bouw Player
        PlayerDTOFactory playerDTOFactory = new PlayerDTOFactory(rooms.ToArray());
        Player player = playerDTOFactory.CreatePlayer(root.player);

        return new GameApplication(rooms, player, connections);
    }
}
