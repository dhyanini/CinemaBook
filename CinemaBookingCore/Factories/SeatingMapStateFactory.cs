using CinemaBookingCore.States;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System.Collections.Generic;

namespace CinemaBookingCore.Factories
{
    public class SeatingMapStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new SeatingMapValidParameterRule(1));
            Rules.Add(new SeatingMapValidRowNoRule(2));
            Rules.Add(new SeatingMapValidSeatsPerRowNoRule(3));
            Rules.Add(new SeatingMapNonZeroAllowedForRowNoRule(4));
            Rules.Add(new SeatingMapRowNoLessThanMaxRowRule(5));
            Rules.Add(new SeatingMapNonZeroAllowedForSeatsPerRowRule(6));
            Rules.Add(new SeatingMapSeatsPerRowNoLessThanMaxSeatsPerRowRule(7));

            return Rules;
        }

        public override RuleEngine CreateRuleEngine()
        {
            return new SeatingMapRuleEngine(CreateRules());
        }
        protected override State CreateState()
        {
            return new SeatingMapState(CreateRuleEngine());
        }
    }
}
