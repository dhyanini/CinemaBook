using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class IsNumberRule : Rule
    {
        public IsNumberRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (!int.TryParse(input, out int _))
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_VALID_INPUT_NO };

            return new Result() { IsValid = true };

        }
    }
}
