using CinemaBookingCore.Factories;
using CinemaBookingCore.Models;
using CinemaBookingCore.Services;
using CinemaBookingCore.Utilities;
using CinemaBookingCore.Validations.RuleEngines;
using System;
using System.Collections.Generic;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.States
{
    public class ReserveBookingState : State
    {        
        private int[,] _seatsAllocated; 

        public ReserveBookingState(RuleEngine ruleEngine) : base(ruleEngine)
        {
            Msg = ACCEPT_BOOKING_MESSAGE;
        }
        public override bool HandleSpecificToState(string input)
        {
            if (input.Trim().Equals(string.Empty))
            {
                List<Booking> bookings = Helper.BuildBookings(_seatsAllocated, Context);

                Context.AddBookingId($"{GIC}{Context.BookingCount}", bookings);
                Context.UpdateSeatAvailability(Context.TotalSeatsToBook);

                Console.WriteLine($"{BOOKING_ID} : {GIC}{Context.BookingCount} confirmed{Environment.NewLine}");

                Context.IncrementBookingCount();
                Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.Options, this));
            }

            else
            {
                //Since All validations have passed, we can convert string to int successfully
                var row = input.Substring(0, 1);
                var selectedRowIndex = Context.RowNameToRowNoMap[Convert.ToChar(row.ToUpper())];
                var selectedCol = int.Parse(input.Substring(1));

                Context.SetSelectedRow(selectedRowIndex);
                Context.SetSelectedCol(selectedCol);
                Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.ReserveBooking, this));
            }
            return true;
        }   

        public override void OnEntry()
        {
            Console.WriteLine(Helper.GetBookingReservedMessage(Context.TotalSeatsToBook, Context.Cinema));
            _seatsAllocated = CinemaSeatsSelectorService.Instance.GetSeats(Context.TotalSeatsToBook, Context.SelectedRow, Context.SelectedCol, Context.Cinema.SeatingMap);

            Console.WriteLine($"{BOOKING_ID} : {GIC}{Context.BookingCount}");
            Console.WriteLine(SELECTED_SEATS);

            DisplaySeatsService.Instance.Display(_seatsAllocated);
            Console.WriteLine(Msg);       
        }
    }
}
