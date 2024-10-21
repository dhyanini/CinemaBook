using CinemaBookingCore.Validations.RuleEngines;
using System;

namespace CinemaBookingCore.States
{
    public class ExitState : State
    {
        public ExitState(RuleEngine ruleEngine) : base(ruleEngine)
        {
        }
        public override bool HandleSpecificToState(string input)
        {
            return false;
        }

        public override void OnEntry()
        {
            Console.WriteLine("Thank you for using GIC Cinemas system. Bye!");
        }
    }
}
