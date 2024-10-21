using CinemaBookingCore.States;
using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Utilities;

namespace CinemaBookingCore.Validations.Rules
{
    public class SeatingMapRowNoLessThanMaxRowRule : Rule
    {
        public SeatingMapRowNoLessThanMaxRowRule(int ruleOrder) : base(ruleOrder)
        {
        }
        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            string[] splitString = Helper.ProcessString(input);

            if (int.TryParse(splitString[1], out int rows) && rows > MAX_ROWS)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_MAX_NO_FOR_ROW };

            return new Result() { IsValid = true };
        }
    }
}
