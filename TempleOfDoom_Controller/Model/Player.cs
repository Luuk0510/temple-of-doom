using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_Game.Model
{
    public class Player
    {
        public int Lives { get; private set; }
        public Room CurrentRoom { get; set; }
        public Coordinates Coordinates { get; private set; }
        public List<ICollectableItem> Inventory { get; private set; }

        private const int DEFAULT_DAMAGE = 1;

        public Player(Room startRoom, Coordinates startCoordinates, int lives)
        {
            CurrentRoom = startRoom;
            Coordinates = startCoordinates;
            Lives = lives;
            Inventory = new List<ICollectableItem>();
        }

        public void AddItem(ICollectableItem item) => Inventory.Add(item);

        public void RemoveLives(int lives) => Lives -= lives;

        /// <summary>
        ///  Verplaats de speler in de opgegeven richting.
        /// </summary>
        public void Move(Direction direction)
        {
            Coordinates newCoordinates = UpdateCoordinates(direction);

            if (HandleInnerDoor(newCoordinates))
            {
                return;
            }

            if (CurrentRoom.AreCoordinatesValid(newCoordinates))
            {
                CurrentRoom.CheckPlayerInteraction(this, newCoordinates);
                Coordinates = newCoordinates;
            }
            else
            {
                CurrentRoom.CheckPlayerConnectionInteraction(this, newCoordinates);
            }

            CurrentRoom.UpdateEnemy();
            CurrentRoom.UpdatePressurePlates(this);
        }

        /// <summary>
        ///  Speler schiet in alle vier richtingen
        /// </summary>
        public void Shoot()
        {
            foreach (Direction dir in new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West })
            {
                CurrentRoom.CheckEnemyHit(UpdateCoordinates(dir), DEFAULT_DAMAGE);
            }

            CurrentRoom.UpdateEnemy();
        }


        public void ChangeRoom(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            CurrentRoom = connection.GetConnectedRoom(CurrentRoom);
        }

        /// <summary>
        ///  Update de spelerpositie wanneer hij in een nieuwe kamer is terechtgekomen.
        /// </summary>
        public void UpdatePlayerPositionForNewRoom(Direction direction)
        {
            Coordinates newCoordinates = CurrentRoom.CalculateNewPlayerPosition(direction);
            CurrentRoom.CheckPlayerInteraction(this, newCoordinates);
            Coordinates = newCoordinates;
        }


        /// <summary>
        ///  Behandel de logica voor een innerdoor. Retourneert true als de move hier al is afgehandeld.
        /// </summary>
        private bool HandleInnerDoor(Coordinates newCoordinates)
        {
            if (CurrentRoom.IsInnerDoor(newCoordinates))
            {
                if (CurrentRoom.CheckPlayerInnerDoorInteraction(this, newCoordinates))
                {
                    Coordinates = newCoordinates;
                }
                CurrentRoom.UpdateEnemy();
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Bereken de nieuwe coördinaten van de speler op basis van de richting.
        /// </summary>
        private Coordinates UpdateCoordinates(Direction direction) => direction switch
        {
            Direction.North => new Coordinates(Coordinates.PositionX, Coordinates.PositionY - 1),
            Direction.East => new Coordinates(Coordinates.PositionX + 1, Coordinates.PositionY),
            Direction.South => new Coordinates(Coordinates.PositionX, Coordinates.PositionY + 1),
            Direction.West => new Coordinates(Coordinates.PositionX - 1, Coordinates.PositionY),
            _ => throw new InvalidOperationException("Ongeldige richting."),
        };
    }
}
