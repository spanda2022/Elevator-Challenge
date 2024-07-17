using ElevatorModule.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorModule.Implementations
{
    public class ElevatorService : IElevatorService
    {
        private readonly ILogger<ElevatorService> _logger;
        private readonly int maxCapacity;
        public ElevatorService(int _maxCapacity,ILogger<ElevatorService> logger=null) {
            maxCapacity= _maxCapacity;
            _logger = logger;
        }

        public bool AddPassenger(int count, Elevator _elevator)
        {
            if (_elevator == null)
            {
                _logger.LogError("Error adding passanger. Elevator object is null.");
                return false;
            }
            try
            {
                if (_elevator.PassengerCount + count <= maxCapacity)
                {
                    _elevator.PassengerCount += count;
                    return true;
                }
            }
            catch(Exception  ex)
            {
                _logger.LogError($"Error adding Passanger.{ex.Message}");
            }
                return false;
            
        }

        public string GetStatus(Elevator elevator)
        {
            if (elevator == null)
            {
                _logger.LogError("Error showing elevator status. Elevator object is null.");
                return string.Empty;
            }
            return $"Elevator {elevator.Id} ({elevator.Type}): Current Floor: {elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, Passengers: {elevator.PassengerCount}";
            
            
        }

        public async Task MoveToFloorAsync(int floor, Elevator elevator)
        {
            if (elevator == null)            
                _logger.LogError("Error moving elavator to floor. Elevator object is null.");

            try
            {
                elevator.CurrentDirection = floor > elevator.CurrentFloor ? Direction.Up : (floor < elevator.CurrentFloor ? Direction.Down : Direction.Stationary);
                while (elevator.CurrentFloor != floor)
                {
                    await Task.Delay(1000); // Simulate time taken to move
                    elevator.CurrentFloor += elevator.CurrentDirection == Direction.Up ? 1 : -1;
                    Console.WriteLine($"Elevator {elevator.Id} moving to floor {elevator.CurrentFloor}. Passengers: {elevator.PassengerCount}");
                }
                elevator.CurrentDirection = Direction.Stationary;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error moving elavator to floor. {ex.Message}");
            }
        }

        public bool RemovePassenger(int count, Elevator _elevator)
        {
            if (_elevator == null)
            {
                _logger.LogError("Error removing passanger from elavator. Elevator object is null.");
                return false;
            }
            if (_elevator.PassengerCount == 0)
            {
                _logger.LogError("Error removing passanger from elavator. Elevator is empty.");
                return false;
            }
            try
            {
                _elevator.PassengerCount = Math.Max(0, _elevator.PassengerCount - count);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error removing passanger from elavator. {ex.Message}");
                return false;
            }
        }
    }
}
