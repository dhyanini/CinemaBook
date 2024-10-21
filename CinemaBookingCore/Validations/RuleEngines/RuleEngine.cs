using CinemaBookingCore.Models;
using CinemaBookingCore.States;
using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rule = CinemaBookingCore.Validations.Rules.Rule;

namespace CinemaBookingCore.Validations.RuleEngines
{
    public abstract class RuleEngine
    {
        public List<Rule> Rules { get; private set; } = new List<Rule>();

        public RuleEngine()
        {

        }
        protected RuleEngine(List<Rule> rules)
        {
            Rules = rules;
        }

        public Result Run(string input, CinemaContext cinemaContext = null)
        {
            Result result = new Result() { IsValid = true };

            //NOTE: Rules are run in precedence order. As some of parsing for valid input is done in initial rules , hence later rules can assume some parsing and validiton is done
            //Otherwise we will have to duplicate  same logic in all Rules again to check what previous rules have done already
            foreach (var rule in Rules.OrderBy(rule => rule.RuleOrder))
            {
                result = rule.Evaluate(input, cinemaContext);
                //As soon as rule fails, return back to user and propagate Error Message to GUI
                if (!result.IsValid || string.IsNullOrEmpty(input))
                    return result;
            }

            return result;
        }
    }
}
