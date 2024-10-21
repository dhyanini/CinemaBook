using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Validations.Rules
{
    public class TicketsAvailabilityRule : Rule
    {
        public TicketsAvailabilityRule(int ruleOrder) : base(ruleOrder)
        {

        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            if (int.TryParse(input, out int selectedOption) && selectedOption > cinemaContext.Cinema.Available)
                return new Result() { IsValid = false, ErrorMessage = $"Sorry there are only {cinemaContext.Cinema.Available} seats available" };

            return new Result() { IsValid = true };
        }
    }
}
