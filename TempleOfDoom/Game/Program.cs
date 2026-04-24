using TempleOfDoom.Controller;

namespace TempleOfDoom.Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();

            gameController.StartGame();
        }
    }
}
