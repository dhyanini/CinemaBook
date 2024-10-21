using CinemaBookingCore.States;
using CinemaBookingCore.Validations.CommonRules;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System.Collections.Generic;

namespace CinemaBookingCore.Factories
{
    public class ExitStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new PassThroughRule(1));
            return Rules;
        }
        
        public override RuleEngine CreateRuleEngine()
        {
            return new ExitStateRuleEngine(CreateRules());
        }

        protected override State CreateState()
        {
            return new ExitState(CreateRuleEngine());
        }

    }
}
