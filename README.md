# Elevator-Challenge
The Elevator Challenge project covers the below...
 1. Modular app 
 2. Console app
 3. Implement SOLID principle
 4. Implemented Unit Tests
 5. Implemented the basic functionality of all the 7 features mentioned in the requirement document.
 6. Implemented the basic central system feature with mutiple Elevators
 7. Implemented Error handling and logging.
 8. Added project to git repo
 
  An Elevator project can be quite big with all the possible features. Because of time constrains I only tried to implement the basic functionalities.
  The exists solution can only cater for "Passanger" elevator type. In the passenger type elevator as well we can add more features in addition to 
  the existing features in the solution.
  The central elevator system on it's own can be a big project with the possible functionalities. The existing central elevator system only cater for basic minimum
  feature.
  
  Existing Features
  1. Central Elevator System
     a. Request Elevator : The passanger need to pass a floornumber and it will check which elevator will available/stationary from the list of elevators and return an elevator object.
	 b. Call Elevator: The passanger need to pass a floornumber and it will internally call the "Request Elevator" which will return the available elevator 
	    and then It will allows the elevator to move to the requested floor by calling the "MoveToElevatorAsync" method of the "EvelatorService" module.
	 c. Display Elevator Statuses: This is display the elevator status of all the elevators the central system. 
  2. Elevator Service 
	 a. MoveToElevatorAsync : This method allows a available elevator to move the requested floor.
	 b. Get Status: This method will return the status of an elevator. This method is called for each elevator in the "Display Elevator Statuses" method.
	 c. Add Passanger: This method allows to add passengers to the elevator if the maximum capacity has not reached.
	 d. Remove Passanger: This method allows to remove passengers from the elevator.
 
  The existing solution has 4 modules. 1. CentralElevatorSystem 2. ElevatorModule 3. ElevatorSolution 4. ElevatorSolution.Test
  
  As per the name 
          CentralElevatorSystem module caters for above functionalities/features related to "Central Elevator System". 
				  ElevatorModule module caters for above functionalities/features related to "Elevator Service".
      	  ElevatorSolution module is the console app for user interaction.
          ElevatorSolution.Test module cater for unit tests for both CentralElevatorSystem and ElevatorModule modules.
          
  There is a flow diagram attached to the solution.
