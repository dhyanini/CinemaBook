using System;
using System.Linq;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Services
{
    public class CinemaSeatsSelectorService
    {
        private DefaultRowSeatsSelectorService dfs = new DefaultRowSeatsSelectorService();
        private SelectedRowSeatsSelectorService sss = new SelectedRowSeatsSelectorService();

        //I am using Singleton pattern here to avoid lot of object allocation although its not a solution requirement
        private static CinemaSeatsSelectorService instance = null;

        private CinemaSeatsSelectorService()
        {
        }

        public static CinemaSeatsSelectorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CinemaSeatsSelectorService();
                }
                return instance;
            }
        }

        public int[,] GetSeats(int totalSeatsToBook, int startRow, int? startColIndex, int[,] rowSeats)
        {

            int rows = rowSeats.GetLength(0);
            int cols = rowSeats.GetLength(1);
            int[,] result = new int[rows, cols];

            Array.Copy(rowSeats, result, rowSeats.Length);

            int totalSeatsBooked = 0;

            int seatsToBook = totalSeatsToBook;

            int[] rowSeatsForCurrentRow = new int[cols];

            for (int rowIndex = startRow; rowIndex >= 0; rowIndex--)
            {
                int[] bookedSeats;
                rowSeatsForCurrentRow = Get1DArrayFrom2DArray(rowSeats, cols, rowSeatsForCurrentRow, rowIndex);

                if (startColIndex.HasValue && rowIndex == startRow)
                {
                    bookedSeats = sss.GetSeats(startColIndex.Value, seatsToBook, rowSeatsForCurrentRow);
                }
                else
                {
                    bookedSeats = dfs.GetDefaultSeats(seatsToBook, rowSeatsForCurrentRow);
                }

                int noOfBookedSeats = GetBookedSeatsCount(bookedSeats);

                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    result[rowIndex, colIndex] = bookedSeats[colIndex];
                }

                totalSeatsBooked = GetTotalSeatsBookedCount(totalSeatsBooked, noOfBookedSeats);

                if (totalSeatsToBook == totalSeatsBooked)
                    break;

                seatsToBook = GetSeatsToBookCount(totalSeatsToBook, totalSeatsBooked);
            }
            return result;
        }

        private static int GetTotalSeatsBookedCount(int totalSeatsBooked, int noOfBookedSeats)
        {
            return totalSeatsBooked + noOfBookedSeats;
        }

        private static int GetSeatsToBookCount(int totalSeatsToBook, int noOfBookedSeats)
        {
            return totalSeatsToBook - noOfBookedSeats;
        }

        private static int[] Get1DArrayFrom2DArray(int[,] twoDArray, int cols, int[] oneDArray, int rowIndex)
        {
            for (int colIndex = 0; colIndex < cols; colIndex++)
            {
                oneDArray[colIndex] = twoDArray[rowIndex, colIndex];
            }
            return oneDArray;
        }

        private static int GetBookedSeatsCount(int[] bookedSeats)
        {
            return bookedSeats.Where(s => s == ALLOCATED).Count();
        }
    }
}
