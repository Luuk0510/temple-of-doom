namespace TempleOfDoom_UserInterface.View;
internal class ConsoleColorSetter
{
    public static void SetConsoleColorBasedOnString(string color)
    {
        Console.ForegroundColor = color.ToLowerInvariant() switch
        {
            "red" => ConsoleColor.Red,
            "blue" => ConsoleColor.Blue,
            "green" => ConsoleColor.Green,
            "yellow" => ConsoleColor.Yellow,
            "purple" => ConsoleColor.Magenta,
            "orange" => ConsoleColor.DarkYellow,
            "white" => ConsoleColor.White,
            "black" => ConsoleColor.Black,
            _ => ConsoleColor.White,
        };
    }
}

