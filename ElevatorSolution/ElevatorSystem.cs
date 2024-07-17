using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSolution
{
    public class Elevator
    {
        public int Id { get; private set; }
        public int CurrentFloor { get; private set; } = 0;
        public int Direction { get; private set; } = 0; // 1 for up, -1 for down, 0 for stationary
        public int PassengerCount { get; private set; } = 0;
        public const int MaxCapacity = 5;

        public Elevator() { }   
        public Elevator(int id)
        {
            Id = id;
        }

        public void MoveToFloor(int floor)
        {
            Direction = floor > CurrentFloor ? 1 : (floor < CurrentFloor ? -1 : 0);
            while (CurrentFloor != floor)
            {
                Thread.Sleep(1000); // Simulate time taken to move
                CurrentFloor += Direction;
                Console.WriteLine($"Elevator {Id} moving to floor {CurrentFloor}. Passengers: {PassengerCount}");
            }
            Direction = 0;
        }

        public bool AddPassenger(int count)
        {
            if (PassengerCount + count <= MaxCapacity)
            {
                PassengerCount += count;
                return true;
            }
            return false;
        }

        public void RemovePassenger(int count)
        {
                PassengerCount = Math.Max(0, PassengerCount - count);
        }

        public string GetStatus()
        {
            return $"Elevator {Id}: Current Floor: {CurrentFloor}, Direction: {Direction}, Passengers: {PassengerCount}";
        }
    }

    public class ElevatorSystem
    {
        private List<Elevator> elevators = new List<Elevator>();
        private int totalFloors;

        public ElevatorSystem(int numberOfElevators, int totalFloors)
        {
            this.totalFloors = totalFloors;
            for (int i = 0; i < numberOfElevators; i++)
            {
                elevators.Add(new Elevator(i + 1));
            }
        }

        public Elevator RequestElevator(int requestedFloor)
        {
            var availableElevators = elevators
                .Where(e => e.Direction == 0)
                .OrderBy(e => Math.Abs(e.CurrentFloor - requestedFloor))
                .ToList();

            if (availableElevators.Count == 0) return null;

            var selectedElevator = availableElevators.First();
            selectedElevator.MoveToFloor(requestedFloor);
            return selectedElevator;
        }

        public void DisplayElevatorStatuses()
        {
            foreach (var elevator in elevators)
            {
                Console.WriteLine(elevator.GetStatus());
            }
        }
    }

    
    
}
