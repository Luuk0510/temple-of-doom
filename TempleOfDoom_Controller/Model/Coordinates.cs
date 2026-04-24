namespace TempleOfDoom_Game.Model
{
    public class Coordinates
    {
        public int PositionX { get; }
        public int PositionY { get; }

        public Coordinates(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}
