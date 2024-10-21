using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaBookingCore.States;
using CinemaBookingCore.Validations.Rules;

namespace CinemaBookingCore.Validations.CommonRules
{
    public class PassThroughRule :Rule
    {
        public PassThroughRule (int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            return new Result() { IsValid = true };

        }
    }
}
