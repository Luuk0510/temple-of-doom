using TempleOfDoom_Game.Model.Observer;

namespace TempleOfDoom_Game.Model.Items
{
    public class PressurePlate : IItem, IToggleDoorsObserver
    {
        public bool IsActivated { get; private set; }
        public Coordinates Coordinates { get; set; }

        private List<IPressurePlateObservable> _observers = new List<IPressurePlateObservable>();

        public PressurePlate(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public void AddObserver(IPressurePlateObservable observer) => _observers.Add(observer);

        public void RemoveObserver(IPressurePlateObservable observer) => _observers.Remove(observer);

        public void NotifyObservers()
        {
            foreach (IPressurePlateObservable observer in _observers)
            {
                observer.Update();
            }
        }

        public void Interact(Player player)
        {
            IsActivated = true;
            NotifyObservers();
        }

        public void Activate()
        {
            IsActivated = true;
            NotifyObservers();
        }

        public void Reset()
        {
            IsActivated = false;
            NotifyObservers();
        }
    }
}