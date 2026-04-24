using TempleOfDoom_DTO.DTO;

namespace TempleOfDoom_DTO
{
    public interface IFileReaderStrategy
    {
        GameDTO ReadFile(string filePath);
    }

}
