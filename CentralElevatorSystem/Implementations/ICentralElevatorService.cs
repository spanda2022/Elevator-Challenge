using ElevatorModule.Implementations;
using ElevatorModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralElevatorSystem.Implementations;

public interface ICentralElevatorService
{
    Elevator RequestElevator(int requestedFloor);
    Task CallElevator(int requestedFloor);
    void DisplayElevatorStatuses(IElevatorService elevatorService);
}
