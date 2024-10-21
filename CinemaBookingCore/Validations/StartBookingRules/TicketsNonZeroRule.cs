using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class TicketsNonZeroRule : Rule
    {
        public TicketsNonZeroRule(int ruleOrder) : base(ruleOrder)
        {
        }
        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (int.TryParse(input, out int selectedOption) && selectedOption < 1)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_NON_ZERO_FOR_NO_OF_TICKETS };

            return new Result() { IsValid = true };
        }
    }
}
