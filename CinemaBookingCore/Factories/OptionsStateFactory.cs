using CinemaBookingCore.States;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Factories
{
    public class OptionsStateFactory : StateFactory
    {
        internal override List<Rule> CreateRules()
        {
            Rules.Add(new IsNumberRule(1));
            Rules.Add(new OptionIsValidRule(2));
            return Rules;
        }

        public override RuleEngine CreateRuleEngine()
        {
            return new OptionsRuleEngine(CreateRules());
        }

        protected override State CreateState()
        {
            return new OptionsState(CreateRuleEngine());
        }
    }
}
