using TempleOfDoom_Game.Model;

namespace TempleOfDoom_UserInterface.IOController
{
    public class IOController
    {
        private readonly Player _player;

        public IOController(Player player)
        {
            _player = player;
        }

        public void HandleInput()
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    _player.Move(Direction.North);
                    break;
                case ConsoleKey.RightArrow:
                    _player.Move(Direction.East);
                    break;
                case ConsoleKey.DownArrow:
                    _player.Move(Direction.South);
                    break;
                case ConsoleKey.LeftArrow:
                    _player.Move(Direction.West);
                    break;
                case ConsoleKey.Spacebar:
                    _player.Shoot();
                    break;
            }
        }

    }
}
