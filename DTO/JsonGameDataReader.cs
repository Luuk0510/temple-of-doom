using System.Text.Json;
using TempleOfDoom_DTO.DTO;

namespace TempleOfDoom_DTO;
public class JsonFileReaderStrategy : IFileReaderStrategy
{
    public GameDTO ReadFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        GameDTO? root = JsonSerializer.Deserialize<GameDTO>(jsonString);

        return root ?? throw new InvalidOperationException("Kon de JSON-inhoud niet deserialiseren naar een GameDTO object.");
    }
}
