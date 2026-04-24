using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Adaptor;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_DTO.DTOFactory
{
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
            List<Room> initializedRooms = new List<Room>();

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
            List<IItem>? createdItems = _itemsDTOFactory.CreateItems(roomDTO.items);
            List<IItem> items = createdItems ?? new List<IItem>();

            List<SpecialFloorTile> specialFloorTiles = new List<SpecialFloorTile>();
            if (roomDTO.specialFloorTiles != null)
            {
                foreach (SpecialFloorTileDTO tileDTO in roomDTO.specialFloorTiles)
                {
                    SpecialFloorTile tile = new SpecialFloorTile(tileDTO.type, tileDTO.x, tileDTO.y);
                    specialFloorTiles.Add(tile);
                }
            }

            List<IEnemy> enemies = new List<IEnemy>();
            if (roomDTO.enemies != null)
            {
                enemies = _enemyDTOFactory.CreateEnemies(roomDTO.enemies);
            }

            return new Room(roomDTO.id, roomDTO.width, roomDTO.height, roomDTO.type, items, specialFloorTiles, enemies);
        }

    }
}
