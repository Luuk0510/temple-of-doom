using TempleOfDoom_Game.Model.Items.CollectableItem;


namespace TempleOfDoom_Game.Model
{
    public class GameApplication
    {
        public Player Player { get; }
        public List<Room> Rooms { get; }
        public List<Connection> Connections { get; }

        private const int NUMBER_OF_STONES = 5;

        public GameApplication(List<Room> rooms, Player player, List<Connection> connections)
        {
            Rooms = rooms;
            Connections = connections;
            Player = player;
        }

        public bool CheckWinCondition()
        {
            int stoneCount = Player.Inventory.Count(item => item is SankaraStone);

            return (stoneCount == NUMBER_OF_STONES);
        }

        public bool CheckLoseCondition()
        {
            return (Player.Lives <= 0);
        }

    }
}