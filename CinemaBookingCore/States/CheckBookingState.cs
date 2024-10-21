using System;
using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Services;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Factories;
using CinemaBookingCore.Utilities;

namespace CinemaBookingCore.States
{
    public class CheckBookingState : State
    {
        public CheckBookingState(RuleEngine ruleEngine) : base(ruleEngine)
        {
            Msg = CHECK_BOOKING_OPTION_MESSAGE;
        }
        public override bool HandleSpecificToState(string input)
        {
            if (!input.Trim().Equals(string.Empty))
            {
                Console.WriteLine($"{BOOKING_ID} : {input}");
                Console.WriteLine(SELECTED_SEATS);

                int[,] result = Helper.GetBookings(input, Context);

                DisplaySeatsService.Instance.Display(result);
                Console.WriteLine();

                Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.CheckBooking, this));
            }
            else
            {
                Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.Options, this));
            }
            return true;
        } 

        public override void OnEntry()
        {
            Console.WriteLine(CHECK_BOOKING_OPTION_MESSAGE);
        }
    }
}
