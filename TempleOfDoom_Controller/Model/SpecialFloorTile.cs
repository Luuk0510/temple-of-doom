namespace TempleOfDoom_Game.Model
{
    public class SpecialFloorTile
    {
        public string Type { get; }
        public int X { get; }
        public int Y { get; }

        public SpecialFloorTile(string type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }
    }
}
