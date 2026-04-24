using CODE_TempleOfDoom_DownloadableContent;

namespace TempleOfDoom_Game.Model.Adaptor
{
    public class EnemyAdapter : IEnemy
    {
        private readonly Enemy _enemy; 

        public EnemyAdapter(Enemy enemy)
        {
            _enemy = enemy;
        }

        public int X => _enemy.CurrentXLocation;
        public int Y => _enemy.CurrentYLocation;

        public bool IsAlive => _enemy.NumberOfLives > 0;

        public void Move()
        {
            IField field = _enemy.Move();
        }

        public void TakeDamage(int damage)
        {
            _enemy.DoDamage(damage);

            if (IsAlive && _enemy.CurrentField == null)
            {
                _enemy.CurrentField = new DummyField();
                _enemy.CurrentField.Item = _enemy;
            }
        }
    }
}
