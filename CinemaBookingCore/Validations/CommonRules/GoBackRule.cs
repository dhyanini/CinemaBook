using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Validations.Rules
{
    public class GoBackRule : Rule
    {
        public GoBackRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return new Result() { IsValid = true };

            return new Result() { IsValid = true };

        }
    }
}
