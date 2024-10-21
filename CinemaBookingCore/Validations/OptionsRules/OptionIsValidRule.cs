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
    public class OptionIsValidRule : Rule
    {
        public OptionIsValidRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (int.TryParse(input, out int selectedOption) && !Enum.IsDefined(typeof(Options), selectedOption))
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_VALID_OPTIONS };

            return new Result() { IsValid = true };

        }
    }
}
