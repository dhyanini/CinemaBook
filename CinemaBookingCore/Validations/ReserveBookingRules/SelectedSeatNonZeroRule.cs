using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;


namespace CinemaBookingCore.Validations.Rules
{
    public class SelectedSeatNonZeroRule : Rule
    {
        public SelectedSeatNonZeroRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (int.TryParse(input.Substring(1), out int rowNum) && rowNum < 1)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_NON_ZERO_FOR_SEATS };

            return new Result() { IsValid = true };
        }
    }
}
