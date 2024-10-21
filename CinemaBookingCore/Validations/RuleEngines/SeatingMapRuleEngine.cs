using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Validations.RuleEngines
{
    public class SeatingMapRuleEngine : RuleEngine
    {
        public SeatingMapRuleEngine(List<Rule> rules) : base(rules)
        {
        }
    }
}
