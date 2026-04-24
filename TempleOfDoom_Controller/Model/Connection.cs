using TempleOfDoom_Game.Model.Doors;

namespace TempleOfDoom_Game.Model
{
    public class Connection
    {
        public int? Within { get; }

        public Dictionary<Room, Direction> RoomDirectionMap { get; }

        public IDoor Door { get; set; } = new Door();

        /// <summary>
        ///  Maakt een verbinding (Connection) met 1 of 2 kamers, afhankelijk van within.
        /// </summary>
        public Connection(Dictionary<Room, Direction> roomDirectionMap, int? within = null)
        {
            RoomDirectionMap = roomDirectionMap;
            Within = within;
        }

        public Room GetConnectedRoom(Room currentRoom)
        {
            foreach (KeyValuePair<Room, Direction> entry in RoomDirectionMap)
            {
                if (entry.Key != currentRoom)
                {
                    return entry.Key;
                }
            }

            throw new InvalidOperationException("The connected room could not be found.");
        }
    }
}
