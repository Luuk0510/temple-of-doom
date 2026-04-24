namespace TempleOfDoom_Game.Model.Observer
{
    public interface IToggleDoorsObserver
    {
        void AddObserver(IPressurePlateObservable observer);
        void RemoveObserver(IPressurePlateObservable observer);
        void NotifyObservers();
    }
}
