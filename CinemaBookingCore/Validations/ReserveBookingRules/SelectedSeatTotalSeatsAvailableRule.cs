using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaBookingCore.States;
using CinemaBookingCore.Validations.Rules;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.ReserveBookingRules
{
    public class SelectedSeatTotalSeatsAvailableRule : Rule
    {
        public SelectedSeatTotalSeatsAvailableRule(int ruleOrder) : base(ruleOrder)
        {
        }

        public override Result Evaluate(string input, CinemaContext cinemaContext = null)
        {
            var rowNumOfSelectedSeat = int.Parse(input.Substring(1));
            var rowAlphabetOfSelectedSeat = input.Substring(0, 1);

            var colIndexOfUnderlyingArray = rowNumOfSelectedSeat - 1;
            var rowIndexOfUnderlyingArray = cinemaContext.Rows - cinemaContext.RowNameToRowNoMap[Convert.ToChar(rowAlphabetOfSelectedSeat.ToUpper())] -1 ;

            int totalSeatsAvailableStartingFromSelectedSeat = 0;

            for ( int rowIndex = 0 ; rowIndex <= rowIndexOfUnderlyingArray; rowIndex++)
            {
                for (int colIndex = cinemaContext.Cols - 1; colIndex >=0  ; colIndex--)
                {
                    if(cinemaContext.Cinema.SeatingMap[rowIndex, colIndex] != OCCUPIED)
                        totalSeatsAvailableStartingFromSelectedSeat++;

                    if (rowIndex == rowIndexOfUnderlyingArray && colIndex == colIndexOfUnderlyingArray)
                        break;
                }
            }

            if (cinemaContext.TotalSeatsToBook > totalSeatsAvailableStartingFromSelectedSeat)
                return new Result() { IsValid = false, ErrorMessage = $"Sorry there are only {totalSeatsAvailableStartingFromSelectedSeat} seats available starting from {input} untill end" };


            return new Result() { IsValid = true };
        }
    }
}
