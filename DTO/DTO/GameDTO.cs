using System.Text.Json.Serialization;

namespace TempleOfDoom_DTO.DTO;

public class GameDTO
{
    [JsonPropertyName("rooms")]
    public required RoomDTO[] Rooms { get; init; }

    [JsonPropertyName("connections")]
    public required ConnectionDTO[] Connections { get; init; }

    [JsonPropertyName("player")]
    public required PlayerDTO Player { get; init; }

    [JsonPropertyName("enemies")]
    public EnemyDTO[] Enemies { get; init; } = [];

    [JsonPropertyName("specialFloorTiles")]
    public SpecialFloorTileDTO[] SpecialFloorTiles { get; init; } = [];
}

public class PlayerDTO
{
    [JsonPropertyName("startRoomId")]
    public int StartRoomId { get; set; }

    [JsonPropertyName("startX")]
    public int StartX { get; set; }

    [JsonPropertyName("startY")]
    public int StartY { get; set; }

    [JsonPropertyName("lives")]
    public int Lives { get; set; }
}

public class RoomDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("items")]
    public ItemDTO[] Items { get; init; } = [];

    [JsonPropertyName("doors")]
    public DoorDTO[] Doors { get; init; } = [];

    [JsonPropertyName("enemies")]
    public EnemyDTO[] Enemies { get; init; } = [];

    [JsonPropertyName("specialFloorTiles")]
    public SpecialFloorTileDTO[] SpecialFloorTiles { get; init; } = [];

}

public class ItemDTO
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }
}

public class ConnectionDTO
{
    [JsonPropertyName("NORTH")]
    public int North { get; set; }

    [JsonPropertyName("SOUTH")]
    public int South { get; set; }

    [JsonPropertyName("doors")]
    public DoorDTO[] Doors { get; init; } = [];

    [JsonPropertyName("WEST")]
    public int West { get; set; }

    [JsonPropertyName("EAST")]
    public int East { get; set; }

    [JsonPropertyName("horizontal")]
    public bool? Horizontal { get; set; }

    [JsonPropertyName("within")]
    public int? Within { get; set; }
}

public class DoorDTO
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("no_of_stones")]
    public int NoOfStones { get; set; }
}

public class EnemyDTO
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("minX")]
    public int MinX { get; set; }

    [JsonPropertyName("minY")]
    public int MinY { get; set; }

    [JsonPropertyName("maxX")]
    public int MaxX { get; set; }

    [JsonPropertyName("maxY")]
    public int MaxY { get; set; }
}

public class SpecialFloorTileDTO
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }
}

