using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class SeatingMapNonZeroAllowedForSeatsPerRowRule : Rule
    {
        public SeatingMapNonZeroAllowedForSeatsPerRowRule(int ruleOrder) : base(ruleOrder)
        {
        }
        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            string[] splitString = Helper.ProcessString(input);

            if (int.TryParse(splitString[2], out int seatsPerRow) && seatsPerRow < 1)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_NON_ZERO_ALLOWED_FOR_SEATS_PER_ROW };

            return new Result() { IsValid = true };
        }
    }
}
