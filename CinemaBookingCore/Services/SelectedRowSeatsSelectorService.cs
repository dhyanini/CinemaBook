using System;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Services
{
    public class SelectedRowSeatsSelectorService
    {
        public int[] GetSeats(int colIndex, int seatsToBook, int[] rowSeats)
        {
            int cols = rowSeats.Length;
            int[] result = new int[cols];
            Array.Copy(rowSeats, result, cols);
            int seatsBooked = 0;

            for (int col = colIndex; col < cols; col++)
            {
                if (seatsToBook == seatsBooked)
                    break;

                if (rowSeats[col] == AVAILABLE)
                {
                    result[col] = ALLOCATED;
                    seatsBooked++;
                }
            }
            return result;
        }
    }
}