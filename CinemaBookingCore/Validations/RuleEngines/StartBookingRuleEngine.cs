using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Validations.RuleEngines
{
    public class StartBookingRuleEngine : RuleEngine
    {       
        public StartBookingRuleEngine(List<Rule> rules) : base(rules)
        {
        }
    }
}
