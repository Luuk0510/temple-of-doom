using TempleOfDoom_Game.Model.Observer;

namespace TempleOfDoom_Game.Model.Doors
{
    public class ToggleDoorDecorator : BaseDoorDecorator, IPressurePlateObservable
    {
        public ToggleDoorDecorator(IDoor decoratedDoor) : base(decoratedDoor)
        {
        }

        public void Update()
        {
            IsOpen = !IsOpen;
        }
    }
}
