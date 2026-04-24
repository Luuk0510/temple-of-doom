namespace TempleOfDoom_Game.Model.Adaptor
{
    public interface IEnemy
    {
        int X { get; }
        int Y { get; }
        bool IsAlive { get; }
        void Move();
        void TakeDamage(int amount);
    }
}
