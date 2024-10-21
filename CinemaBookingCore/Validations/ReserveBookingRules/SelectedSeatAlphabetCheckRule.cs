using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public class SelectedSeatAlphabetCheckRule : Rule
    {
        public SelectedSeatAlphabetCheckRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            var rowAlphabet = input.Substring(0, 1);

            if (!cinemaContext.RowNameToRowNoMap.ContainsKey(Convert.ToChar(rowAlphabet.ToUpper())))
            {
                return new Result() { IsValid = false, ErrorMessage = $"Please input valid Seat. Only alphabet allowed in first letter, input provided was {rowAlphabet}" };

            }

            return new Result() { IsValid = true };
        }
    }
}
