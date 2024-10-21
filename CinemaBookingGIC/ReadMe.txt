Project Running Enviornments
==============================
1. This is .Net Code
2. This is built using .NET5 
3. To open Solution, Compile and Run : This was built using  Microsoft Visual Studio Community 2019 Version 16.10.3. Hence need 2019 Version 16.10.3 or above for successfull build
4. Environment required is Windows Platform
5. Please make sure .NET is installed on Windows machine. By default it is installed on Windows machine
6. Startup Project is CinemaBookingGIC. 
7. Entry Point of project is Program.cs under CinemaBookingGIC Project
8. For running the Project :
   a) First Build the project using Visual Studio. Open CinemaBookingGIC\CinemaBookingGIC.sln in Visual Studio.
   b) Then can run either from Visual Studio itself or directly running the \CinemaBookingGIC\bin\Debug\net5.0\CinemaBookingGIC.exe 


Design Assumptions and Details
=================================

1. CinemaBookingGIC Project doesnt have any business logic. It is just the Console Host i.e entry point to run the application.

2. This solution follows headless architecture as main business logic is in CinemaBookingCore Project Library. 
   This makes the solution future extensible as we can plug in any head i.e Web/Desktop/Mobile GUI which can interact with CinemaBookingCore Project Library .
   In that future scenario, we can just use a message queue or REST API or GRPN ednpoint to bridge between headless CinemaBookingCore Project Library and the GUI.
   GUI will publish message on message queue and CinemaBookingCore  will keep listening on that . But project requirement is not regarding that but this keep it extensible.

3. CinemaBookingTests is the test project having both Unit and EndToEnd System Tests. It is also a client of CinemaBookingCore Project Library.

4. I have used State Design Pattern to map out the Cinema Booking System stages in below order:

   a) SeatingMap State: When application starts . It immediately moves to SeatingMap State and waits for User Input via Console. 

   b) Options State : When user inputs correct parameters => Then system enters into Options State . We then show Welcome Menu message 
					  and ask user to provide any of three choices : [1] Book Tickets [2] Check Booking [3] Exit

   c) StartBooking State : If user selects [1] Book Tickets in Step (b), then system enter into StartBooking State. It then waits for user input to provide number of seats to book.

   d) CheckBooking State : If user selects [2] Check Booking in Step (b) , then system enter into CheckBooking State. It then waits for user input to provide bookingid to check.

   e) Exit State: If user selects [3] Exit in Step (b) , then system enter into Exit State and exits.

   f) ReserveBooking State : 
			While waiting for input in StartBooking State , when user inputs blank => then we book the system selected default seats and move back to Options State again.
			While waiting for input in StartBooking State , when user inputs another seat to select => then we enter into ReserveBooking State again and selects new seats based on
			selected seat and waits for user input again.

 For more Details , i have created a State Transition Table which transitions states based on input and current state only.
 This helps in reducing any future bugs also as new developer who might not be aware wont be able to let system transation into invalid state even by accident.
 State Transition Allowable logic is centralized in one class "StateFactoryCache.cs".
 GetNextState method in "StateFactoryCache.cs" => This has all the logic of State Transition.

I had also thought of using StateMachine earlier but i think using State Pattern is better than using StateMachine as current solution makes it more loosely coupled and extensible.

5. Since there are many validations in each state for inputs . i have used specific RuleEngine for each State . Every RuleEngine has number of rules as its children
   Whenever user provided any input, system tries to move to next allowable state but before that we run the ruleengine against the input , if all rules are passed successfully,
   then only system moves to new allowable state , otherwise it remains in same current state and ask user to provide valid input.
   Since each state has its specific RuleEngine and each RuleEngine has its specific Rules , i have reactored that into Abstract factory Pattern as 
   State/RuleEngine/Rules are all related based on the State. 
   Also in future, if we want to add any new validation, we just have to create a new rule and add it to specific RuleEngine of the State for which validation has to be done.
   Plugging in rules will make it more maintainable and very less prone to bugs as we are touching only small part of system which is quite self conatined and isolated.

6. All The Logic for SeatSelection , DisplaySeats, Creating Rows/SeatsPerRow for Cinema  etc have been moved under Services folder.

   a) DisplaySeatsService : This is used to display the Cinema Seats i.e mostly GUI specific
                            This is made Singleton as we dont need to create many objects of this to reduce Memory allocation
							Also it doesnt have any Specific State, only behaviour.

   b) SeatingMapService : This service is used to create underlying cinema seats 2D array when user provided Rows and SeatPerRow.
                          This is made Singleton as we dont need to create many objects of this to reduce Memory allocation
						  Also it doesnt have any Specific State, only behaviour.

   c) DefaultRowSeatsSelectorService : This service is used to select middle most possible seats in a given row only

   d) SelectedRowSeatsSelectorService : This service is used to select middle most possible seats in a row when user provides selected Seat

   e) CinemaSeatsSelectorService : This is the parent service which is used to select middle most possible rows in whole cinema.
                                   It uses both DefaultRowSeatsSelectorService and SelectedRowSeatsSelectorService underneath.
								   It uses SelectedRowSeatsSelectorService when user provides selected seat only for that row which has selected seat
								   It uses DefaultRowSeatsSelectorService for all rows except selected seat row.
								   This is made Singleton as we dont need to create many objects of this to reduce Memory allocation
								   Also it doesnt have any Specific State, only behaviour.

7. Some of Common functions , i have moved to Helper.cs class under Utilities folder. I deliberately made this class as static so that
  it doesnt hold any state underneath. It is just input and output function, which makes it idempotent. 
  This is kind of following functional programming paradigm as its very easy to understand because there is no state hidden inside the
  method and all logic is dependent on input parameters, which reduces cognitive load as well.
  Also this makes it easy to test and we dont need any mocks as its not dependent on any other dependent object underneath.



  Future Design Ideas
  =====================
  1. I would like to introduce some sort of asynchronous running class =>  lets saye QueueProcessor.cs which have its own internal queue and keep listening on it.
	 QueueProcessor will have current System State as its internal field.
     Input from console/GUI will be sent to the queue during lifetime of application. As soon as input is there in the queue => QueueProcessor will wake up and 
	 based on input => do the state validations and then transitions to next state if allowed.
	 This will make it fully decoupled from Client.
	 
