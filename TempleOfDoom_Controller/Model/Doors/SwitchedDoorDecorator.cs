using TempleOfDoom_Game.Model.Items;
using TempleOfDoom_Game.Model.Observer;

namespace TempleOfDoom_Game.Model.Doors
{
    public class SwitchedDoorDecorator : BaseDoorDecorator, IPressurePlateObservable
    {
        private readonly List<PressurePlate> _pressurePlates;

        public SwitchedDoorDecorator(IDoor decoratedDoor, List<PressurePlate> pressurePlates)
            : base(decoratedDoor)
        {
            _pressurePlates = pressurePlates;
            IsOpen = false;
        }

        public override bool CanPass(Player player)
        {
            return IsOpen;
        }

        public void Update()
        {
            if (_pressurePlates.All(plate => plate.IsActivated))
            {
                IsOpen = true;
            }
        }
    }
}
