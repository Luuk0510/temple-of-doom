using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Adaptor;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_DTO.DTOFactory;

public class RoomFactory
{
    private readonly ItemsDTOFactory _itemsDTOFactory;
    private readonly EnemyDTOFactory _enemyDTOFactory;

    public RoomFactory(ItemsDTOFactory itemsDtoFactory, EnemyDTOFactory enemyDtoFactory)
    {
        _itemsDTOFactory = itemsDtoFactory;
        _enemyDTOFactory = enemyDtoFactory;
    }

    /// <summary>
    ///  Maakt een lijst van Room objecten aan op basis van de RoomDTO array.
    /// </summary>
    public List<Room> CreateRooms(RoomDTO[] roomsDTO)
    {
        List<Room> initializedRooms = [];

        foreach (RoomDTO roomDTO in roomsDTO)
        {
            Room newRoom = CreateRoom(roomDTO);
            initializedRooms.Add(newRoom);
        }

        return initializedRooms;
    }

    /// <summary>
    ///  Bouwt 1 Room object op basis van een RoomDTO, inclusief items, specialFloorTiles en enemies.
    /// </summary>
    private Room CreateRoom(RoomDTO roomDTO)
    {
        List<IItem> items = _itemsDTOFactory.CreateItems(roomDTO.Items);

        List<SpecialFloorTile> specialFloorTiles = [];
        foreach (SpecialFloorTileDTO tileDTO in roomDTO.SpecialFloorTiles)
        {
            SpecialFloorTile tile = new SpecialFloorTile(tileDTO.Type, tileDTO.X, tileDTO.Y);
            specialFloorTiles.Add(tile);
        }

        List<IEnemy> enemies = _enemyDTOFactory.CreateEnemies(roomDTO.Enemies);

        return new Room(roomDTO.Id, roomDTO.Width, roomDTO.Height, roomDTO.Type, items, specialFloorTiles, enemies);
    }

}
