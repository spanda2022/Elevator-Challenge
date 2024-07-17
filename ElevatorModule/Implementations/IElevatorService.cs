using ElevatorModule.Models;
using System.Threading.Tasks;

namespace ElevatorModule.Implementations;
public interface IElevatorService
{
    Task MoveToFloorAsync(int floor,Elevator elevator);
    bool AddPassenger(int count, Elevator elevator);
    bool RemovePassenger(int count, Elevator elevator);
    string GetStatus(Elevator elevator);

}