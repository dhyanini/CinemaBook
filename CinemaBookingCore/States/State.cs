using CinemaBookingCore.Validations.RuleEngines;
using System;

namespace CinemaBookingCore.States
{
    public abstract class State
    {
        public CinemaContext Context { get;  set; }
        public RuleEngine RuleEngine { get;  set; }
        public virtual string Msg { get; protected set; } = "";

        public State()
        {
        }
        public State(RuleEngine ruleEngine)
        {
            RuleEngine = ruleEngine;
        }
        public void SetContext(CinemaContext context)
        {
            Context = context;
            OnEntry();
        }

        public bool Validate(string input, string msg = null)
        {
            var validationResult = RuleEngine.Run(input, Context);

            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.ErrorMessage);
                if (!string.IsNullOrEmpty(msg))
                    Console.WriteLine(msg);
                return true;
            }

            return false;
        }

        public abstract void OnEntry();

        public bool Handle(string input)
        {
            if (Validate(input, Msg))
                return true;

            return HandleSpecificToState(input);
        }
        public abstract bool HandleSpecificToState(string input);
    }
}
