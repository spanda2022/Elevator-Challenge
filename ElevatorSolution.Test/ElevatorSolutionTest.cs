using CentralElevatorSystem.Implementations;
using ElevatorModule.Implementations;
using ElevatorModule.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

namespace ElevatorSolution.Test
{
    public class ElevatorSolutionTest
    {                
        private const int nTotalFloors=5;
        private const int nMaxCapacity=10;
        ICentralElevatorService _testCentralElevatorSystem;
        IElevatorService _testElevatorService;
        protected readonly ILoggerFactory _loggerFactory = new NullLoggerFactory();
        public ElevatorSolutionTest()
        {
            //add or initialize central elavator system..            
            _testElevatorService = new ElevatorService(nMaxCapacity, _loggerFactory.CreateLogger<ElevatorService>());
            
        }
        private Elevator CreateSampleModel(int cntPassanger)
        {
            Elevator elevator = new Elevator()
            {
                Id = 1,
                Type=ElevatorType.Passenger,
                CurrentDirection=Direction.Stationary,
                CurrentFloor=0,
                PassengerCount= cntPassanger,
            };
            return elevator;
        }

        [Fact]
        public void RequestElevator_Fact_Success()
        {
            // Arrange            
            _testCentralElevatorSystem = new CentralElevatorService(2, nTotalFloors, _testElevatorService, nMaxCapacity, _loggerFactory.CreateLogger<CentralElevatorService>());

            // Act
            Elevator e = _testCentralElevatorSystem.RequestElevator(2);
            if (e !=null)
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.NotEqual(nMaxCapacity, e.PassengerCount);
            }
        }
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(3)]
        public void RequestElevator_Theory_Success(int iFloor)
        {
            // Arrange            
            _testCentralElevatorSystem = new CentralElevatorService(2, nTotalFloors, _testElevatorService, nMaxCapacity, _loggerFactory.CreateLogger<CentralElevatorService>());

            // Act
            Elevator e = _testCentralElevatorSystem.RequestElevator(iFloor);
            if (e != null)
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);                
                Assert.NotEqual(nMaxCapacity, e.PassengerCount);
            }
        }

        [Fact]
        public void RequestElevator_Fact_FailWithIncorrectFloorRequest()
        {
            // Arrange            
            _testCentralElevatorSystem = new CentralElevatorService(2, nTotalFloors, _testElevatorService, nMaxCapacity, _loggerFactory.CreateLogger<CentralElevatorService>());

            // Act
            Elevator e = _testCentralElevatorSystem.RequestElevator(6);

            if (e != null)
            {
                // Assert
                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.NotEqual(6, nTotalFloors);
            }
        }

        [Theory]
        [InlineData(7)]
        [InlineData(6)]
        public void RequestElevator_Theory_Fail(int iFloor)
        {
            // Arrange            
            _testCentralElevatorSystem = new CentralElevatorService(2, nTotalFloors, _testElevatorService, nMaxCapacity, _loggerFactory.CreateLogger<CentralElevatorService>());

            // Act
            Elevator e = _testCentralElevatorSystem.RequestElevator(iFloor);

            if (e != null)
            {
                // Assert
                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.NotEqual(iFloor, nTotalFloors);
            }
        }

        [Fact]
        public void AddPassangersToElevator_Fact_Success()
        {
            // Arrange
            Elevator e = CreateSampleModel(1);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.AddPassenger(2,e);
            if ((flg == true) && ((iPassanger + 2) <= nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger+2, e.PassengerCount);
            }
        }


        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(6)]
        public void AddPassangersToElevator_Theory_Success(int cntPassanger)
        {
            // Arrange
            Elevator e = CreateSampleModel(1);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.AddPassenger(cntPassanger, e);
            if ((flg == true) && ((iPassanger+cntPassanger) <= nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger+cntPassanger, e.PassengerCount);
            }
        }

        [Fact]
        public void AddPassangersToElevator_Fact_Fail()
        {
            // Arrange
            Elevator e = CreateSampleModel(nMaxCapacity);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.AddPassenger(2, e);
            if ((flg == false) && ((iPassanger+2)> nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.NotEqual(iPassanger+2, e.PassengerCount);
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(6)]
        public void AddPassangersToElevator_Theory_Fail(int cntPassanger)
        {
            // Arrange
            Elevator e = CreateSampleModel(nMaxCapacity);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.AddPassenger(cntPassanger, e);
            if ((flg == false) && ((iPassanger + cntPassanger) > nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.NotEqual(iPassanger + cntPassanger, e.PassengerCount);
            }
        }

        [Fact]
        public void RemovePassangersFromElevator_Fact_Success()
        {
            // Arrange
            Elevator e = CreateSampleModel(8);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.RemovePassenger(2, e);
            if ((flg == true) && ((iPassanger - 2) <= nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger - 2, e.PassengerCount);
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(6)]
        public void RemovePassangersFromElevator_Theory_Success(int cntPassanger)
        {

            // Arrange
            Elevator e = CreateSampleModel(10);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.RemovePassenger(cntPassanger, e);
            if ((flg == true) && ((iPassanger - cntPassanger) <= nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger - cntPassanger, e.PassengerCount);
            }
        }

        [Fact]
        public void RemovePassangersFromElevator_Fact_Fail()
        {
            // Arrange
            Elevator e = CreateSampleModel(0);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.RemovePassenger(2, e);
            if ((flg == false) && ((iPassanger - 2) > nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger, e.PassengerCount);
            }
        }

        [Theory]
        [InlineData(3)]
        [InlineData(9)]
        [InlineData(6)]
        public void RemovePassangersFromElevator_Theory_Fail(int cntPassanger)
        {

            // Arrange
            Elevator e = CreateSampleModel(0);
            int iPassanger = e.PassengerCount;
            // Act
            var flg = _testElevatorService.RemovePassenger(cntPassanger, e);
            if ((flg == false) && ((iPassanger - cntPassanger) > nMaxCapacity))
            {
                // Assert

                Assert.NotNull(e);
                Assert.IsType<Elevator>(e);
                Assert.Equal(iPassanger, e.PassengerCount);
            }
        }

    }
}