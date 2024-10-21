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
    public class SeatingMapValidRowNoRule : Rule
    {
        public SeatingMapValidRowNoRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            string[] splitString = Helper.ProcessString(input);

            if (!int.TryParse(splitString[1], out int rows))
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_VALID_ROW_NO };

            return new Result() { IsValid = true };
        }
    }
}
