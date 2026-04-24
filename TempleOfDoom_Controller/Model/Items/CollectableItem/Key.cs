using TempleOfDoom_Game.Model.Items.CollectableItem;

namespace TempleOfDoom_Game.Model.Items
{
    public class Key : ICollectableItem
    {
        public Coordinates Coordinates { get; set; }
        public string Color { get; set; }

        public Key(Coordinates coordinates, string color)
        {
            Color = color;
            Coordinates = coordinates;
        }

        /// <summary>
        ///  Zorgt dat de speler de sleutel oppakt en toevoegd aan inventory
        /// </summary>
        public void Interact(Player player)
        {
            player.AddItem(this);
            player.CurrentRoom.RemoveItem(this);
        }
    }
}
