using CinemaBookingCore.States;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Validations.Rules;
using System.Collections.Generic;

namespace CinemaBookingCore.Factories
{
    public abstract class StateFactory
    {
        internal List<Rule> Rules { get; private set; } = new List<Rule>();
        protected internal State State { get; protected set; } = null;
        internal abstract List<Rule> CreateRules();
        public abstract RuleEngine CreateRuleEngine();
        protected abstract State CreateState();

        public State GetState()
        {
            return CreateState();
        }
    }
}
