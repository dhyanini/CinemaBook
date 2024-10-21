using CinemaBookingCore.States;
using CinemaBookingCore.Validations.ReserveBookingRules;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Factories
{
    public class ReserveBookingStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new GoBackRule(1));
            Rules.Add(new SelectedSeatValidRule(2));
            Rules.Add(new SelectedSeatAlphabetCheckRule(3));
            Rules.Add(new SelectedSeatNonZeroRule(4));
            Rules.Add(new SelectedSeatAvailabilityRule(5));
            Rules.Add(new SelectedSeatTotalSeatsAvailableRule(6));
            return Rules;
        }
        public override RuleEngine CreateRuleEngine()
        {
            return new ChangeSeatRuleEngine(CreateRules());
        }

        protected override State CreateState()
        {
            return new ReserveBookingState(CreateRuleEngine());
        }
    }
}
