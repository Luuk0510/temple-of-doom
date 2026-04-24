namespace TempleOfDoom_Game.Model.Items.CollectableItem
{
    public class SankaraStone : ICollectableItem
    {
        public Coordinates Coordinates { get; set; }
        
        public SankaraStone(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        /// <summary>
        ///  Zorgt dat de speler de steen oppakt en toevoegd aan de inventory
        /// </summary>
        public void Interact(Player player)
        {
            player.AddItem(this);
            player.CurrentRoom.RemoveItem(this);
        }
    }
}
