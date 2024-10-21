using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class SeatingMapValidParameterRule : Rule
    {
        public SeatingMapValidParameterRule(int ruleOrder) : base(ruleOrder)
        {
        }
        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            string[] splitString = Helper.ProcessString(input);

            if (splitString.Length != 3)
                return new Result() { IsValid = false, ErrorMessage = ERROR_MESSAGE_INPUT_3PARAMETERS };

            return new Result() { IsValid = true };
        }
    }
}
