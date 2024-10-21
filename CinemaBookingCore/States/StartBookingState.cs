using CinemaBookingCore.Factories;
using CinemaBookingCore.Utilities;
using CinemaBookingCore.Validations.RuleEngines;
using System;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.States
{
    public class StartBookingState : State
    {
        public StartBookingState(RuleEngine ruleEngine) : base(ruleEngine)
        {
            Msg = BOOKING_OPTION_MESSAGE;
        }
        public override bool HandleSpecificToState(string input)
        {
            if (input.Trim().Equals(string.Empty))
            {
                Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.Options, this));
                return true;
            }

            //Since All validations have passed, we can convert string to int successfully
            var totalSeatsToBook = int.Parse(input);

            Context.SetTotalSeatsToBook(totalSeatsToBook);
            Context.SetSelectedRow(0);
            Context.SetSelectedCol(null);
            Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.ReserveBooking, this));
            return true;
        }
        public override void OnEntry()
        {
            Console.WriteLine(Msg);
        }
    }
}
