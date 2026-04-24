namespace TempleOfDoom_Game.Model.Doors
{
    public class ClosingGateDoorDecorator : BaseDoorDecorator
    {
        private bool _hasPassed;

        public ClosingGateDoorDecorator(IDoor decoratedDoor) : base(decoratedDoor)
        {
            _hasPassed = false;
        }

        /// <summary>
        ///  De speler kan de deur één keer passeren. Daarna is de deur gesloten.
        /// </summary>
        public override bool CanPass(Player player)
        {
            if (_hasPassed)
            {
                return false;
            }

            _hasPassed = true;

            return base.CanPass(player);
        }
    }
}
