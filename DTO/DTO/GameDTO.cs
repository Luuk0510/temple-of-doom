namespace TempleOfDoom_DTO.DTO;
public class GameDTO
{
    public required RoomDTO[] rooms { get; init; }
    public required ConnectionDTO[] connections { get; init; }
    public required PlayerDTO player { get; init; }
    public EnemyDTO[] enemies { get; init; } = [];
    public SpecialFloorTileDTO[] specialFloorTiles { get; init; } = [];
}

public class PlayerDTO
{
    public int startRoomId { get; set; }
    public int startX { get; set; }
    public int startY { get; set; }
    public int lives { get; set; }
}

public class RoomDTO
{
    public int id { get; set; }
    public required string type { get; init; }
    public int width { get; set; }
    public int height { get; set; }
    public ItemDTO[] items { get; init; } = [];
    public DoorDTO[] doors { get; init; } = [];
    public EnemyDTO[] enemies { get; init; } = [];
    public SpecialFloorTileDTO[] specialFloorTiles { get; init; } = [];

}

public class ItemDTO
{
    public required string type { get; init; }
    public int damage { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public string? color { get; set; }
}

public class ConnectionDTO
{
    public int NORTH { get; set; }
    public int SOUTH { get; set; }
    public DoorDTO[] doors { get; init; } = [];
    public int WEST { get; set; }
    public int EAST { get; set; }
    public bool? horizontal { get; set; }
    public int? within { get; set; }
}

public class DoorDTO
{
    public required string type { get; init; }
    public string? color { get; set; }
    public int no_of_stones { get; set; }
}

public class EnemyDTO
{
    public required string type { get; init; }
    public int x { get; set; }
    public int y { get; set; }
    public int minX { get; set; }
    public int minY { get; set; }
    public int maxX { get; set; }
    public int maxY { get; set; }
}

public class SpecialFloorTileDTO
{
    public required string type { get; init; }
    public int x { get; set; }
    public int y { get; set; }
}


