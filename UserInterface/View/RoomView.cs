using System.Text;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Adaptor;
using TempleOfDoom_Game.Model.Doors;
using TempleOfDoom_Game.Model.Items;
using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_UserInterface.View;
/// <summary>
/// De RoomView klasse zorgt voor het visueel weergeven van kamers, muren, deuren, vijanden en items in de console
/// </summary>
public class RoomView
{
    private readonly Player _player;

    public RoomView(Player player)
    {
        _player = player;
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void DisplayRoom()
    {
        for (int y = 0; y < _player.CurrentRoom.Height; y++)
        {
            for (int x = 0; x < _player.CurrentRoom.Width; x++)
            {
                DisplaySymbolAt(x, y);
            }
            Console.WriteLine();
        }
    }

    private void DisplaySymbolAt(int x, int y)
    {
        if (_player.Coordinates.PositionX == x && _player.Coordinates.PositionY == y)
        {
            WriteColored("X ", ConsoleColor.DarkYellow);
        }
        else if (IsOuterWall(x, y))
        {
            DisplayWallOrDoor(x, y);
        }
        else if (DisplaySpecialFloorTile(x, y))
        {
            return;
        }
        else if (DisplayEnemy(x, y))
        {
            return;
        }
        else if (DisplayItem(x, y))
        {
            return;
        }
        else
        {
            Console.Write("  ");
        }
    }

    private bool IsOuterWall(int x, int y) => x == 0 || y == 0 || x == _player.CurrentRoom.Width - 1 || y == _player.CurrentRoom.Height - 1;

    private void DisplayWallOrDoor(int x, int y)
    {
        foreach (KeyValuePair<Connection, Direction> connectionEntry in _player.CurrentRoom.ConnectionDirectionMap)
        {
            if (DoorMatchesPosition(connectionEntry.Value, x, y))
            {
                DisplayDoorSymbol(connectionEntry.Key);
                return;
            }
        }

        WriteColored("# ", ConsoleColor.Yellow);
    }

    private bool DoorMatchesPosition(Direction direction, int x, int y)
    {
        int midWidth = _player.CurrentRoom.Width / 2;
        int midHeight = _player.CurrentRoom.Height / 2;

        return direction switch
        {
            Direction.North => y == 0 && x == midWidth,
            Direction.South => y == _player.CurrentRoom.Height - 1 && x == midWidth,
            Direction.West => x == 0 && y == midHeight,
            Direction.East => x == _player.CurrentRoom.Width - 1 && y == midHeight,
            _ => false,
        };
    }

    private void DisplayDoorSymbol(Connection connection)
    {
        if (connection.Door.Color != null)
        {
            ConsoleColorSetter.SetConsoleColorBasedOnString(connection.Door.Color);
        }

        string symbol = connection.Door switch
        {
            ClosingGateDoorDecorator _ => "∩ ",
            SwitchedDoorDecorator _ => "~ ",
            ToggleDoorDecorator _ => "⊥ ",
            Door _ => "  ",
            _ => "= ",
        };

        Console.Write(symbol);
        Console.ResetColor();
    }

    private bool DisplaySpecialFloorTile(int x, int y)
    {
        SpecialFloorTile? tile = _player.CurrentRoom.SpecialFloorTiles?.FirstOrDefault(t => t.X == x && t.Y == y);
        if (tile == null)
        {
            return false;
        }

        switch (tile.Type.ToLower())
        {
            case "wall":
                WriteColored("# ", ConsoleColor.Yellow);
                break;
            case "innerdoor":
                WriteColored("⊥ ", ConsoleColor.Red);
                break;
            default:
                Console.Write("  ");
                break;
        }
        return true;
    }

    private bool DisplayEnemy(int x, int y)
    {
        IEnemy? enemy = _player.CurrentRoom.Enemies?.FirstOrDefault(e => e.X == x && e.Y == y && e.IsAlive);
        if (enemy == null)
        {
            return false;
        }

        WriteColored("E ", ConsoleColor.Cyan);
        return true;
    }

    private bool DisplayItem(int x, int y)
    {
        IItem? item = _player.CurrentRoom.Items?.FirstOrDefault(i => i.Coordinates.PositionX == x && i.Coordinates.PositionY == y);
        if (item == null)
        {
            return false;
        }

        switch (item)
        {
            case DisappearingBoobyTrap _:
                WriteColored("@ ", ConsoleColor.White);
                break;
            case BoobyTrap _:
                WriteColored("O ", ConsoleColor.White);
                break;
            case PressurePlate _:
                WriteColored("T ", ConsoleColor.Gray);
                break;
            case Key key:
                ConsoleColorSetter.SetConsoleColorBasedOnString(key.Color);
                Console.Write("K ");
                Console.ResetColor();
                break;
            case SankaraStone _:
                WriteColored("S ", ConsoleColor.DarkMagenta);
                break;
            default:
                Console.Write("  ");
                break;
        }

        return true;
    }

    private void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}
