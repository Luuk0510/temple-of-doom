using TempleOfDoom_Game.Model.Adaptor;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_Game.Model
{
    public class Room
    {
        public int Id { get; }
        public string Type { get; }
        public int Width { get; }
        public int Height { get; }
        public Dictionary<Connection, Direction> ConnectionDirectionMap { get; }
        public List<IItem> Items { get; }
        public List<SpecialFloorTile> SpecialFloorTiles { get; }
        public List<IEnemy> Enemies { get; }

        public Room(int id, int width, int height, string type, List<IItem> items, List<SpecialFloorTile> specialFloorTiles, List<IEnemy> enemies)
        {
            Id = id;
            Width = width;
            Height = height;
            Type = type;
            Items = items ?? new List<IItem>();
            ConnectionDirectionMap = new Dictionary<Connection, Direction>();
            SpecialFloorTiles = specialFloorTiles ?? new List<SpecialFloorTile>();
            Enemies = enemies ?? new List<IEnemy>();
        }

        /// <summary>
        ///  Verwijder een item uit de kamer.
        /// </summary>
        public void RemoveItem(IItem item) => Items.Remove(item);

        /// <summary>
        ///  Player interactie met items en vijanden op de opgegeven coördinaten.
        /// </summary>
        public void CheckPlayerInteraction(Player player, Coordinates coordinates)
        {
            foreach (IItem item in Items.ToList())
            {
                if (coordinates.PositionX == item.Coordinates.PositionX &&
                    coordinates.PositionY == item.Coordinates.PositionY)
                {
                    item.Interact(player);
                }
            }

            foreach (IEnemy enemy in Enemies)
            {
                if (enemy.X == coordinates.PositionX && enemy.Y == coordinates.PositionY)
                {
                    player.RemoveLives(1);
                }
            }
        }

        /// <summary>
        ///  Controleer of de speler door een inner door mag gaan.
        /// </summary>
        public bool CheckPlayerInnerDoorInteraction(Player player, Coordinates coordinates)
        {
            if (!IsInnerDoor(coordinates))
            {
                return false;
            }

            Connection innerDoorConnection = ConnectionDirectionMap.Keys.FirstOrDefault(conn => conn.Within.HasValue && conn.Within.Value == Id)
                ?? throw new InvalidOperationException("Geen inner door connection gevonden.");

            if (innerDoorConnection == null || !innerDoorConnection.Door.IsOpen)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///  Voeg een connectie toe aan de dictionary (met bijbehorende richting).
        /// </summary>
        public void AddConnectionDirectionMap(Connection connection, Direction direction) => ConnectionDirectionMap.Add(connection, direction);

        /// <summary>
        ///  Controleer of een gegeven coördinaat binnen de kamer en niet geblokkeerd is.
        /// </summary>
        public bool AreCoordinatesValid(Coordinates coordinates)
        {
            // Kamergrenzen
            if (coordinates.PositionX < 0 || coordinates.PositionX >= Width ||
                coordinates.PositionY < 0 || coordinates.PositionY >= Height)
            {
                return false;
            }
            // Buitenmuur
            if (coordinates.PositionX == 0 || coordinates.PositionX == Width - 1 ||
                coordinates.PositionY == 0 || coordinates.PositionY == Height - 1)
            {
                return false;
            }
            // Wall tile
            if (SpecialFloorTiles.Any(tile =>
                tile.Type.Equals("wall", StringComparison.OrdinalIgnoreCase) &&
                tile.X == coordinates.PositionX &&
                tile.Y == coordinates.PositionY))
            {
                return false;
            }
            // Innerdoor
            if (IsInnerDoor(coordinates))
            {
                Connection? innerDoorConnection = ConnectionDirectionMap.Keys.FirstOrDefault(conn => conn.Within.HasValue && conn.Within.Value == Id);

                if (innerDoorConnection == null || !innerDoorConnection.Door.IsOpen)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  Controleer of de speler via newPlayerCoordinates een connectie kan activeren
        /// </summary>
        public void CheckPlayerConnectionInteraction(Player player, Coordinates newPlayerCoordinates)
        {
            foreach (KeyValuePair<Connection, Direction> entry in ConnectionDirectionMap)
            {
                Connection connection = entry.Key;
                Direction direction = entry.Value;

                Coordinates connectionCoords = GetConnectionCoordinates(direction);
                if (connectionCoords.PositionX == newPlayerCoordinates.PositionX &&
                    connectionCoords.PositionY == newPlayerCoordinates.PositionY)
                {
                    if (connection.Door.CanPass(player))
                    {
                        player.ChangeRoom(connection);
                        player.UpdatePlayerPositionForNewRoom(direction);
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///  Bereken de nieuwe player-positie binnen de kamer na een overgang via de connectie.
        /// </summary>
        public Coordinates CalculateNewPlayerPosition(Direction direction)
        {
            Coordinates connectionPosition = FindConnectionPosition(OppositeDirection(direction));
            int newX = connectionPosition.PositionX;
            int newY = connectionPosition.PositionY;

            switch (direction)
            {
                case Direction.North:
                    newY -= 1;
                    break;
                case Direction.East:
                    newX += 1;
                    break;
                case Direction.South:
                    newY += 1;
                    break;
                case Direction.West:
                    newX -= 1;
                    break;
            }

            // Houd de speler binnen de kamer
            newX = Math.Clamp(newX, 1, Width - 2);
            newY = Math.Clamp(newY, 1, Height - 2);

            return new Coordinates(newX, newY);
        }

        /// <summary>
        ///  Update al het vijand-gedrag in deze kamer.
        /// </summary>
        public void UpdateEnemy()
        {
            foreach (IEnemy enemy in Enemies)
            {
                if (enemy.IsAlive)
                {
                    enemy.Move();
                }
            }
        }

        /// <summary>
        ///  Controleer of er een vijand op de opgegeven coördinaten staat en breng damage toe.
        /// </summary>
        public void CheckEnemyHit(Coordinates coordinates, int damage)
        {
            foreach (IEnemy enemy in Enemies)
            {
                if (enemy.IsAlive && enemy.X == coordinates.PositionX && enemy.Y == coordinates.PositionY)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        /// <summary>
        ///  Update alle drukplaten in de kamer: als er twee of meer zijn,
        ///  activeer ze enkel als ze allebei worden bediend.
        /// </summary>
        public void UpdatePressurePlates(Player player)
        {
            List<PressurePlate> plates = Items.OfType<PressurePlate>().ToList();
            if (plates.Count < 2)
            {
                return;
            }

            bool IsOccupied(PressurePlate plate) => (player.Coordinates.PositionX == plate.Coordinates.PositionX && 
                player.Coordinates.PositionY == plate.Coordinates.PositionY) ||
                Enemies.Any(e => e.X == plate.Coordinates.PositionX && e.Y == plate.Coordinates.PositionY);

            bool allPressed = plates.All(IsOccupied);

            foreach (PressurePlate plate in plates)
            {
                if (allPressed && !plate.IsActivated)
                {
                    plate.Activate();
                }
                else if (!allPressed && plate.IsActivated)
                {
                    plate.Reset();
                }
            }
        }

        /// <summary>
        ///  Controleert of er op de opgegeven coördinaten een innerdoor-tegel staat.
        /// </summary>
        public bool IsInnerDoor(Coordinates coordinates)
        {
            return SpecialFloorTiles.Any(tile =>
                tile.Type.Equals("innerdoor", StringComparison.OrdinalIgnoreCase) &&
                tile.X == coordinates.PositionX &&
                tile.Y == coordinates.PositionY);
        }

        /// <summary>
        ///  Vind de coördinaten van de connectie in de kamer voor een bepaalde richting.
        /// </summary>
        private Coordinates FindConnectionPosition(Direction direction)
        {
            foreach (KeyValuePair<Connection, Direction> entry in ConnectionDirectionMap)
            {
                if (entry.Value == direction)
                {
                    int x = -1;
                    int y = -1;
                    switch (direction)
                    {
                        case Direction.North:
                            x = Width / 2;
                            y = 0;
                            break;
                        case Direction.East:
                            x = Width - 1;
                            y = Height / 2;
                            break;
                        case Direction.South:
                            x = Width / 2;
                            y = Height - 1;
                            break;
                        case Direction.West:
                            x = 0;
                            y = Height / 2;
                            break;
                    }
                    return new Coordinates(x, y);
                }
            }
            throw new InvalidOperationException("Connection niet gevonden.");
        }

        /// <summary>
        ///  Geef de connectie-coördinaten in de kamer.
        /// </summary>
        private Coordinates GetConnectionCoordinates(Direction direction)
        {
            int x = Width / 2;
            int y = Height / 2;

            switch (direction)
            {
                case Direction.North:
                    y = 0;
                    break;
                case Direction.East:
                    x = Width - 1;
                    break;
                case Direction.South:
                    y = Height - 1;
                    break;
                case Direction.West:
                    x = 0;
                    break;
            }

            return new Coordinates(x, y);
        }

        /// <summary>
        ///  Bepaalt de tegengestelde richting (North vs South, East vs West).
        /// </summary>
        private Direction OppositeDirection(Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.East => Direction.West,
                Direction.South => Direction.North,
                Direction.West => Direction.East,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), "Ongeldige direction"),
            };
        }

    }
}
