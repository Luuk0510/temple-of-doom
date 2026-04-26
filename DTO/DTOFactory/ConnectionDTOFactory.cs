using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Doors;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_DTO.DTOFactory;

/// <summary>
///  Maakt Connection objecten aan op basis van ConnectionDTOs.
/// </summary>
public class ConnectionDTOFactory
{
    private readonly List<Room> _rooms;
    private readonly DoorDTOFactory _doorDTOFactory;

    public ConnectionDTOFactory(List<Room> rooms, DoorDTOFactory doorDTOFactory)
    {
        _rooms = rooms;
        _doorDTOFactory = doorDTOFactory;
    }

    /// <summary>
    ///  Creëert Connection objecten op basis van ConnectionDTO[].
    /// </summary>
    public List<Connection> CreateConnections(ConnectionDTO[] connectionsDTO)
    {
        List<Connection> initializedConnections = [];

        foreach (ConnectionDTO dto in connectionsDTO)
        {
            Connection newConnection = CreateConnectionFromDTO(dto);
            initializedConnections.Add(newConnection);
        }

        return initializedConnections;
    }

    /// <summary>
    ///  Gebaseerd op een enkele ConnectionDTO bouw je een Connection object.
    /// </summary>
    private Connection CreateConnectionFromDTO(ConnectionDTO connectionDTO)
    {
        Dictionary<Room, Direction> roomDirectionMap = BuildRoomDirectionMap(connectionDTO);

        // Check het aantal rooms
        int expectedRooms = (connectionDTO.Within.HasValue && connectionDTO.Within.Value != 0) ? 1 : 2;
        if (roomDirectionMap.Count != expectedRooms)
        {
            throw new InvalidOperationException("Invalid connection data.");
        }

        // Bouw de connection
        Connection newConnection = new Connection(roomDirectionMap, connectionDTO.Within);

        // Bouw deur
        IDoor? door = BuildDoorIfNeeded(connectionDTO, roomDirectionMap);
        if (door != null)
        {
            newConnection.Door = door;
        }

        // Update alle betrokken Rooms
        foreach (KeyValuePair<Room, Direction> entry in roomDirectionMap)
        {
            entry.Key.AddConnectionDirectionMap(newConnection, GetReversedDirection(entry.Value));
        }

        return newConnection;
    }

    /// <summary>
    /// Bepaalt of het om een innerdoor of een normale verbinding gaat.
    /// </summary>
    private Dictionary<Room, Direction> BuildRoomDirectionMap(ConnectionDTO connectionDTO)
    {
        int expectedRooms = (connectionDTO.Within.HasValue && connectionDTO.Within.Value != 0) ? 1 : 2;
        Dictionary<Room, Direction> map = new Dictionary<Room, Direction>();

        if (connectionDTO.Within is int innerRoomId && innerRoomId != 0)
        {
            Room? innerRoom = _rooms.FirstOrDefault(r => r.Id == innerRoomId);
            if (innerRoom != null)
            {
                map.Add(innerRoom, Direction.North);
            }
        }
        else
        {
            // Normale verbinding
            Direction[] directions = { Direction.North, Direction.West, Direction.South, Direction.East };
            foreach (Direction direction in directions)
            {
                int index = GetIndexFromDirection(connectionDTO, direction);
                if (index != 0)
                {
                    Room room = GetRoomById(index);
                    map.Add(room, direction);
                }
            }
        }
        return map;
    }

    /// <summary>
    /// Maakt optioneel een deur aan vanuit de ConnectionDTO.
    /// </summary>
    private IDoor? BuildDoorIfNeeded(ConnectionDTO connectionDTO, Dictionary<Room, Direction> roomDirectionMap)
    {
        if (connectionDTO.Doors.Length == 0)
        {
            return null;
        }

        List<PressurePlate> connectionPressurePlates = roomDirectionMap.Keys
            .Where(r => r.Items != null)
            .SelectMany(r => r.Items.OfType<PressurePlate>())
            .ToList();

        return _doorDTOFactory.InitializeDoors(connectionDTO.Doors, connectionPressurePlates);
    }


    /// <summary>
    ///  Haal de index uit ConnectionDTO bij een gegeven Direction.
    /// </summary>
    private int GetIndexFromDirection(ConnectionDTO connection, Direction direction) => direction switch
    {
        Direction.North => connection.North,
        Direction.East => connection.East,
        Direction.South => connection.South,
        Direction.West => connection.West,
        _ => 0,
    };

    private Room GetRoomById(int roomId)
    {
        return _rooms.FirstOrDefault(room => room.Id == roomId)
            ?? throw new InvalidOperationException($"Room met id {roomId} niet gevonden.");
    }

    /// <summary>
    ///  Geef de omgekeerde richting terug.
    /// </summary>
    private Direction GetReversedDirection(Direction direction) => direction switch
    {
        Direction.North => Direction.South,
        Direction.East => Direction.West,
        Direction.South => Direction.North,
        Direction.West => Direction.East,
        _ => Direction.North,
    };
}
