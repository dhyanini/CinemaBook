using CinemaBookingCore.States;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System.Collections.Generic;

namespace CinemaBookingCore.Factories
{
    public class CheckBookingStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new GoBackRule(1));
            Rules.Add(new CheckBookingRule(2));
            return Rules;
        }
        public override RuleEngine CreateRuleEngine()
        {
            return new CheckBookingRuleEngine(CreateRules());
        }
        protected override State CreateState()
        {
            return new CheckBookingState(CreateRuleEngine());
        }
    }
}
