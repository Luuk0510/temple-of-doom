using CODE_TempleOfDoom_DownloadableContent;
using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model.Adaptor;

namespace TempleOfDoom_DTO.DTOFactory;

public class EnemyDTOFactory
{
    private const int DEFAULT_ENEMY_LIVES = 3;

    /// <summary>
    ///  Maakt een lijst van IEnemy objecten aan op basis van de doorgegeven EnemyDTO array.
    /// </summary>
    public List<IEnemy> CreateEnemies(EnemyDTO[] enemiesDTO)
    {
        if (enemiesDTO.Length == 0)
        {
            return [];
        }

        List<IEnemy> enemies = [];

        foreach (EnemyDTO enemyDTO in enemiesDTO)
        {
            IEnemy? newEnemy = CreateEnemyFromDTO(enemyDTO, DEFAULT_ENEMY_LIVES);
            if (newEnemy != null)
            {
                enemies.Add(newEnemy);
            }
        }

        return enemies;
    }

    /// <summary>
    ///  Bouwt op basis van 1 EnemyDTO een IEnemy object
    ///  </summary>
    private IEnemy? CreateEnemyFromDTO(EnemyDTO enemyDTO, int enemyLives)
    {
        switch (enemyDTO.Type.ToLowerInvariant())
        {
            case "horizontal":
                {
                    HorizontallyMovingEnemy enemy = new HorizontallyMovingEnemy(enemyLives, enemyDTO.X, enemyDTO.Y, enemyDTO.MinX, enemyDTO.MaxX);
                    enemy.CurrentField = new DummyField(); 
                    return new EnemyAdapter(enemy);
                }
            case "vertical":
                {
                    VerticallyMovingEnemy enemy = new VerticallyMovingEnemy(enemyLives, enemyDTO.X, enemyDTO.Y, enemyDTO.MinY, enemyDTO.MaxY);
                    enemy.CurrentField = new DummyField();
                    return new EnemyAdapter(enemy);
                }
            default:
                return null;
        }
    }

}
