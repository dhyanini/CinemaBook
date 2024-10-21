using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class SelectedSeatValidRule : Rule
    {
        public SelectedSeatValidRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (input.Length > 3)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_VALID_SEAT };

            if (!int.TryParse(input.Substring(1), out int colNo))
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_VALID_SEAT };

            return new Result() { IsValid = true };
        }
    }
}
