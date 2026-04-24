using System.Text;
using TempleOfDoom_Game.Model;
using TempleOfDoom_Game.Model.Items;
using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_UserInterface.View;
/// <summary>
/// De GameView klasse regelt het tekenen van het volledige game-scherm inclusief kamer, spelerinformatie en statusbalken in de console.
/// </summary>
public class GameView
{
    public RoomView RoomView;

    private readonly string _filePath;

    private readonly Player _player;

    public GameView(Player player, string filePath)
    {
        _player = player;
        _filePath = filePath;
        RoomView = new RoomView(_player);
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void Draw()
    {
        DrawHeader();
        RoomView.DisplayRoom();
        DrawPlayerInfo();
        DrawFooter();
    }

    private void DrawHeader()
    {
        Console.WriteLine("Welcome to the Temple of Doom!");
        Console.WriteLine($"Current level: {_filePath}");
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine("");
    }

    private void DrawPlayerInfo()
    {
        Console.Write($"Lives: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(_player.Lives);
        Console.ResetColor();

        Console.WriteLine("");

        DrawInventory("Key", item => item is Key key, "K ", key => ConsoleColorSetter.SetConsoleColorBasedOnString(key.Color));
        DrawInventory("sankaraStone", item => item is SankaraStone, "S ", key => Console.ForegroundColor = ConsoleColor.DarkMagenta);

        Console.WriteLine("");
    }

    private void DrawInventory(string itemName, Func<ICollectableItem, bool> filter, string symbol, Action<Key> colorSetter)
    {
        Console.Write($"{itemName}: ");
        foreach (ICollectableItem collectableItem in _player.Inventory)
        {
            if (filter(collectableItem))
            {
                colorSetter(collectableItem as Key);
                Console.Write(symbol);
                Console.ResetColor();
            }
        }
        Console.WriteLine("");
    }

    private void DrawFooter()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine("A game for the course Code Development (23/25) by Luuk Spruijtenburg.");
        Console.WriteLine(new string('-', Console.WindowWidth));
        Console.WriteLine(new string('-', Console.WindowWidth));
    }

}
