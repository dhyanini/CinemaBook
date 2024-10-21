using CinemaBookingCore.States;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Factories
{    public class StartBookingStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new GoBackRule(1));
            Rules.Add(new IsNumberRule(2));
            Rules.Add(new TicketsNonZeroRule(3));
            Rules.Add(new TicketsAvailabilityRule(4));
            return Rules;
        }

        public override RuleEngine CreateRuleEngine()
        {
            return new StartBookingRuleEngine(CreateRules());

        }
        protected override State CreateState()
        {
            return new StartBookingState(CreateRuleEngine());
        }
    }
}
