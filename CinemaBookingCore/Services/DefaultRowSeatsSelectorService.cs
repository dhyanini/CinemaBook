using System;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Services
{
    public class DefaultRowSeatsSelectorService
    {
        public int[] GetDefaultSeats(int seatsToBook, int[] rowSeats)
        {
            var isEven = rowSeats.Length % 2 == 0;

            int startIndex = rowSeats.Length / 2;

            int seatsBooked = 0;
            int index = 0;

            int[] result = new int[rowSeats.Length];
            Array.Copy(rowSeats, result, rowSeats.Length);

            while (index < startIndex)
            {
                int indexToCheck = startIndex + index;
                seatsBooked = CheckSeatAvailability(result[indexToCheck], indexToCheck, result, seatsBooked);
                if (seatsToBook == seatsBooked)
                    break;

                if (isEven)
                {
                    indexToCheck = startIndex - index - 1;
                    seatsBooked = CheckSeatAvailability(result[indexToCheck], indexToCheck, result, seatsBooked);
                    if (seatsToBook == seatsBooked)
                        break;
                }
                else
                {
                    indexToCheck = startIndex - index;
                    seatsBooked = CheckSeatAvailability(result[indexToCheck], indexToCheck, result, seatsBooked);
                    if (seatsToBook == seatsBooked)
                        break;
                }

                index++;
            }
            return result;
        }

        public int CheckSeatAvailability(int seatToCheck, int indexToCheck, int[] result, int seatsBooked)
        {
            if (seatToCheck == AVAILABLE)
            {
                result[indexToCheck] = ALLOCATED;
                seatsBooked++;
            }
            return seatsBooked;
        }
    }
}