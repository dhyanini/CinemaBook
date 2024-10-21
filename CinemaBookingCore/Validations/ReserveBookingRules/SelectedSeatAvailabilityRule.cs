using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;


namespace CinemaBookingCore.Validations.Rules
{
    public class SelectedSeatAvailabilityRule : Rule
    {
        public SelectedSeatAvailabilityRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            var row = input.Substring(0, 1);

            if (int.TryParse(input.Substring(1), out int rowNum) && rowNum > cinemaContext.Cinema.SeatsPerRow)
                return new Result() { IsValid = false, ErrorMessage = $"Please input valid Seat. You selected {input} but seats untill {row}{cinemaContext.Cinema.SeatsPerRow} available in this Row" };


            return new Result() { IsValid = true };
        }
    }
}
