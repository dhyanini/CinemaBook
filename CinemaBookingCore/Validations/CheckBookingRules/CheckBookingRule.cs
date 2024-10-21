using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class CheckBookingRule : Rule
    {
        public CheckBookingRule(int ruleOrder) : base(ruleOrder)
        {

        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (!cinemaContext.BookingIdToSeatingMap.ContainsKey(input))
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_BOOKING_NOT_EXISTS };

            return new Result() { IsValid = true };
        }
    }
}
