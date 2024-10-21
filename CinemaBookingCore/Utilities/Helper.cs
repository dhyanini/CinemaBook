using CinemaBookingCore.Models;
using CinemaBookingCore.States;
using CinemaBookingCore.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Utilities
{
    public class Helper
    {
        public static readonly Dictionary<int, char> SeatStateToDisplayMap = new Dictionary<int, char>()
        {
            {AVAILABLE, '.' },
            {ALLOCATED, 'O' },
            {OCCUPIED, '#'}
        };

        public static string GetBookingMessage(Cinema cinema)
        {
            return $"[1] Book Tickets for { cinema.MovieName } ({cinema.Available} seats available)";
        }

        public static string GetBookingReservedMessage(int totalSeatsToBook, Cinema cinema)
        {
            return $"Successfully reserved {totalSeatsToBook} { cinema.MovieName } tickets";

        }

        public static string[] ProcessString(string input)
        {
            string trimmedString = input.Trim();

            // Split the string by whitespace (handles variable-length spaces)
            string[] splitString = trimmedString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return splitString;
        }

        public static bool CheckIsNumber(string input)
        {
            return int.TryParse(input, out int _);
        }

        public static Dictionary<char, int> BuildRowNameToRowNoMap( int rows)
        {
            int asciiVal = ASCII_VALUE;
            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(asciiVal + rowIndex), rowIndex);
            }
            return rowNameToRowNoMap;
        }


        public static List<Booking> BuildBookings(int[,] seatsAllocated, CinemaContext context)
        {
            List<Booking> bookings = new List<Booking>();

            for (int rowIndex = 0; rowIndex < context.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < context.Cols; colIndex++)
                {
                    if (seatsAllocated[rowIndex, colIndex] == ALLOCATED)
                    {
                        context.Cinema.SeatingMap[rowIndex, colIndex] = OCCUPIED;
                        bookings.Add(new Booking() { Row = rowIndex, Col = colIndex });
                    }
                }
            }
            return bookings;
        }

        public static int[,] GetBookings(string bookingId, CinemaContext context)
        {
            int[,] result = new int[context.Rows, context.Cols];
            var rowSeats = context.Cinema.SeatingMap;

            Array.Copy(rowSeats, result, rowSeats.Length);

            foreach (var booking in context.BookingIdToSeatingMap[bookingId])
            {
                result[booking.Row, booking.Col] = ALLOCATED;
            }

            return result;
        }
    }
}
