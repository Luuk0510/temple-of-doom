using TempleOfDoom_DTO;
using TempleOfDoom_Game.Model;
using TempleOfDoom_UserInterface.IOController;
using TempleOfDoom_UserInterface.View;

namespace TempleOfDoom.Controller;
public class GameController
{
    private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "JSON", "TempleOfDoom.json");

    private GameReader _gameReader;
    private GameApplication _gameApplication;
    private IOController _iOController;

    public GameController()
    {
        IFileReaderStrategy readerStrategy = new JsonFileReaderStrategy();
        _gameReader = new GameReader(readerStrategy);
        _gameApplication = _gameReader.ReadGame(_filePath);
        _iOController = new IOController(_gameApplication.Player);
    }

    internal void StartGame()
    {
        bool gameIsOver = false;
        while (!gameIsOver)
        {
            Console.Clear();

            GameView gameView = new GameView(_gameApplication.Player, _filePath);
            gameView.Draw();

            if (_gameApplication.CheckWinCondition())
            {
                Console.WriteLine("You have won the game!");
                gameIsOver = true;
            }
            else if (_gameApplication.CheckLoseCondition())
            {
                Console.WriteLine("You have lost the game! Press Enter to restart or any other key to exit.");
                gameIsOver = true;
            }
            else
            {
                _iOController.HandleInput();
            }

        }
    }
}

