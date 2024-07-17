using CentralElevatorSystem.Implementations;
using ElevatorModule.Implementations;
using ElevatorModule.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{    
    static void Main(string[] args)
    {

        //setup our DI
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        //vaidate maxCapacity
        string maxCapacity = config["MAX_CAPACITY"] ?? string.Empty;
        if (string.IsNullOrEmpty(maxCapacity))
            throw new InvalidOperationException("Please specify a valid maximum capacity of an elevator.");

        int nMaxCapacity;
        bool isMaxCapacityNumeric = int.TryParse(maxCapacity, out nMaxCapacity);
        if (!isMaxCapacityNumeric)
            throw new InvalidOperationException("Please specify valid value for maximum capacity of an elevator.");

        //validate Total Floors
        string totalFloors = config["TOTAL_FLOORS"] ?? string.Empty;
        if (string.IsNullOrEmpty(totalFloors))
            throw new InvalidOperationException("Please specify valid value for total floors.");

        int nTotalFloors;
        bool isTotFloorsNumeric = int.TryParse(totalFloors, out nTotalFloors);
        if (!isTotFloorsNumeric)
            throw new InvalidOperationException("Please specify valid value for total floors.");

        ServiceProvider? serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddTransient<IElevatorService, ElevatorService>(s =>
            {
                return new ElevatorService(nMaxCapacity,
                                    s.GetService<ILogger<ElevatorService>>());
            })
            .AddTransient<ICentralElevatorService, CentralElevatorService>(s =>
            {
                //validate No Of Elevators building
                string noOfElevators = config["NO_OF_ELEVATORS"] ?? string.Empty;
                if (string.IsNullOrEmpty(noOfElevators))
                    throw new InvalidOperationException("Please specify a valid value for No of Elevators in the building.");

                int nNoOfElevators;
                bool isNoOfElevatorsNumeric = int.TryParse(noOfElevators, out nNoOfElevators);
                if (!isNoOfElevatorsNumeric)
                    throw new InvalidOperationException("Please specify a valid value for No of Elevators in the building.");


                return new CentralElevatorService(nNoOfElevators, nTotalFloors,  s.GetService<ElevatorService>(), nMaxCapacity,
                    s.GetService<ILogger<CentralElevatorService>>());
            })
            .BuildServiceProvider();

        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();

        //end of setup our DI

        logger.LogDebug("Welcome to the Elevator Control System");
        Console.WriteLine("Welcome to the Elevator Control System");
        
        //add or initialize central elavator system..
        var centralElavatorSystem = serviceProvider.GetService<ICentralElevatorService>();
        var elavatorService = serviceProvider.GetService<IElevatorService>();

        //passanger interaction starts
        while (true)
        {
            //display status of each elevator added to the centra system.
            centralElavatorSystem.DisplayElevatorStatuses(elavatorService);
            
            Console.WriteLine("Call an elevator to which floor? (0 to exit)");
            if (int.TryParse(Console.ReadLine(), out int requestedFloor) && requestedFloor != 0)
            {
                if (requestedFloor >= 0 && requestedFloor <= nTotalFloors)
                {
                    //passanger requested for one of the available elevators for a specific floor
                    Elevator elevator = centralElavatorSystem.RequestElevator(requestedFloor);
                    if (elevator == null)
                    {
                        Console.WriteLine("No available elevator at the moment.");
                        break;
                    }
                    else if (elevator != null || elevator.PassengerCount < nMaxCapacity)
                    {
                        //capture how many passangers wanted to go in to the elevator.
                        Console.WriteLine($"Elevator {elevator.Id} arrived at floor {requestedFloor}.");
                        Console.WriteLine("How many passengers are going in to the elevator?");
                        int passengerAddCount = int.Parse(Console.ReadLine());

                        //capture how many passangers wanted to go out of the elevator.
                        Console.WriteLine("How many passengers are going out of the elevator?");
                        int passengerRemoveCount = int.Parse(Console.ReadLine());

                        //call RemovePassange
                        elavatorService.RemovePassenger(passengerRemoveCount, elevator);

                        //call AddPassanger
                        if (elavatorService.AddPassenger(passengerAddCount, elevator))
                        {
                            Console.WriteLine($"Elevator {elevator.Id} loaded {passengerAddCount} passengers.");
                        }
                        else
                        {
                            Console.WriteLine($"Cannot load {passengerAddCount} passengers. Exceeds capacity.");
                        }
                    }                    
                }
                else
                {
                    Console.WriteLine("Invalid floor number.");
                }
            }
            else
            {
                break;
            }
        }
    }
}