using CentralElevatorSystem.Implementations;
using ElevatorModule.Implementations;
using ElevatorModule.Models;
using Microsoft.Extensions.Logging;

namespace CentralElevatorSystem.Implementations;

public class CentralElevatorService :ICentralElevatorService
{
    private readonly List<Elevator> elevators = new List<Elevator>();
    private readonly IElevatorService elevatorService;
    private readonly int totalFloors;
    private readonly int numberOfElevators;
    private readonly int maxCapacity;
    private readonly ILogger<ICentralElevatorService> logger;
    public CentralElevatorService(int _numberOfElevators, int _totalFloors,IElevatorService _elevatorService,int _maxCapacity, ILogger<ICentralElevatorService> _logger=null)
    {
        this.elevatorService = _elevatorService;
        this.totalFloors = _totalFloors;
        this.maxCapacity = _maxCapacity;
        this.numberOfElevators = _numberOfElevators;
        this.logger = _logger;

        if (numberOfElevators <= 0)
            logger.LogError("No of elavators can always be greater than 0.");

        if (totalFloors <= 1)
            logger.LogError("No of floors should always be greater than 1.");

        for (int i = 0; i < _numberOfElevators; i++)
        {
            // Create all elevators as passenger type initially
            elevators.Add(new Elevator(i + 1, ElevatorType.Passenger));
        }
    }

    public async Task CallElevator(int requestedFloor)
    {
        try
        {
            var elevator = RequestElevator(requestedFloor);
            if (elevator != null)
            {
                await this.elevatorService.MoveToFloorAsync(requestedFloor,elevator);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error calling elavator. {ex.Message}");
        }
    }

    public void DisplayElevatorStatuses(IElevatorService elevatorService)
    {
        if (elevators == null)
            logger.LogError("Invalid elavator list");
        else if (elevators.Count <= 0)
            logger.LogError("No elevator added to the central elevator system");

        foreach (var elevator in elevators)
        {
            Console.WriteLine(elevatorService.GetStatus(elevator));
        }
    }
    public Elevator RequestElevator(int requestedFloor)
    {
        if (elevators == null)
            logger.LogError("Invalid elavator list");
        else if (elevators.Count <= 0)
            logger.LogError("No elevator added to the central elevator system");

        try
        {
            var availableElevators = elevators
                .Where(e => e.CurrentDirection == Direction.Stationary && e.PassengerCount < maxCapacity)
                .OrderBy(e => Math.Abs(e.CurrentFloor - requestedFloor))
                .ToList();

            if(availableElevators.Count == 0)
                logger.LogError("Requested elavator does not exits in central elevator system");

            return availableElevators.FirstOrDefault();
        }
        catch(Exception ex)
        {
            logger.LogError($"Error requesting elavator. {ex.Message}");
            return null;
        }
    }

}
