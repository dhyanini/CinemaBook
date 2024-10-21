using CinemaBookingCore.Factories;
using CinemaBookingCore.Models;
using CinemaBookingCore.Utilities;
using CinemaBookingCore.Validations.RuleEngines;
using System;
using System.Text;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.States
{
    public class OptionsState : State
    {
        public OptionsState(RuleEngine ruleEngine) : base(ruleEngine)
        {

        }
        public override bool HandleSpecificToState(string input)
        {
            //Since All validations have passed, we can now convert string to int successfully
            var selectedOption = int.Parse(input);
            Context.TransitionTo(StateFactoryCache.Instance.GetState((StateType)selectedOption, this));
            return true;
        }
        public override void OnEntry()
        {
            StringBuilder sb = BuildMainMenu(Context.Cinema);
            Console.WriteLine(sb);
        }
        private  StringBuilder BuildMainMenu(Cinema gicCinema)
        {
            return new StringBuilder().Append(WELCOME_MESSAGE).Append(Environment.NewLine).Append(Helper.GetBookingMessage(gicCinema)).Append(Environment.NewLine).
                            Append(CHECK_BOOKING_MESSAGE).Append(Environment.NewLine).Append(EXIT_MESSAGE).Append(Environment.NewLine).
                            Append(ENTER_SELECTION_MESSAGE);
        }
    }
}
