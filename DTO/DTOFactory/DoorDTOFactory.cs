using TempleOfDoom_DTO.DTO;
using TempleOfDoom_Game.Model.Doors;
using TempleOfDoom_Game.Model.Items;

namespace TempleOfDoom_DTO.DTOFactory;
public class DoorDTOFactory
{
    /// <summary>
    ///  Maakt en decoreert een deur op basis van de door gegevens in de DoorDTO[] 
    /// </summary>
    public IDoor? InitializeDoors(DoorDTO[] doors, List<PressurePlate> pressurePlatesList)
    {
        if (doors.Length == 0)
        {
            return null;
        }

        IDoor newDoor = new Door();
        bool toggleDoor = false;

        foreach (DoorDTO doorDTO in doors)
        {
            switch (doorDTO.Type.ToLowerInvariant())
            {
                case "colored":
                    newDoor = new ColoredDoorDecorator(newDoor, doorDTO.Color ?? throw new InvalidOperationException("Een colored door moet een color hebben."));
                    break;
                case "open on stones in room":
                    newDoor = new OpenOnStonesInRoomDoorDecorator(newDoor, doorDTO.NoOfStones);
                    break;
                case "toggle":
                    toggleDoor = true;
                    break;
                case "closing gate":
                    newDoor = new ClosingGateDoorDecorator(newDoor);
                    break;
                case "open on odd":
                    newDoor = new OpenOnOddDoorDecorator(newDoor);
                    break;
                case "switched":
                    newDoor = new SwitchedDoorDecorator(newDoor, pressurePlatesList);
                    break;
            }
        }

        RegisterDoorObservers(ref newDoor, toggleDoor, pressurePlatesList);

        return newDoor;
    }

    /// <summary>
    ///  Registreert de benodigde observers bij de toggle of switched door.
    /// </summary>
    private void RegisterDoorObservers(ref IDoor door, bool toggleDoor, List<PressurePlate> pressurePlatesList)
    {
        if (toggleDoor)
        {
            // Maak van de huidige deur een ToggleDoorDecorator
            door = new ToggleDoorDecorator(door);
            foreach (PressurePlate plate in pressurePlatesList)
            {
                plate.AddObserver((ToggleDoorDecorator)door);
            }
        }
        else if (door is SwitchedDoorDecorator switchedDoor)
        {
            // Registreer drukplaten bij de switched deur
            foreach (PressurePlate plate in pressurePlatesList)
            {
                plate.AddObserver(switchedDoor);
            }
        }
    }
}
