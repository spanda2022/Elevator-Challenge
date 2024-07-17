using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorModule.Models;

public class Elevator
{
    public int Id { get; set; }
    public int CurrentFloor { get; set; } = 0;
    public Direction CurrentDirection { get; set; } = Direction.Stationary;
    public int PassengerCount { get; set; } = 0;
   
    public ElevatorType Type { get; set; }
    public Elevator()
    {

    }
    public Elevator(int id, ElevatorType type)
    {
        Id = id;
        Type = type;
    }

}
