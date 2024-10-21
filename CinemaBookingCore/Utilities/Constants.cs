using CinemaBookingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Utilities
{ 

    public class Constants
    {
        public const string APP_START_MESSAGE = "Please define movie title and seating map in [Title] [Row] [SeatsPerRow] format: ";
        public const string WELCOME_MESSAGE = "Welcome to  GIC Cinemas ";

        public const string CHECK_BOOKING_MESSAGE = "[2] Check Bookings";
        public const string EXIT_MESSAGE = "[3] Exit";
        public const string ENTER_SELECTION_MESSAGE = "Please enter your Selection";
        public const string BOOKING_OPTION_MESSAGE = "Enter number of tickets to book, or enter blank to go back to main menu :";
        public const string ACCEPT_BOOKING_MESSAGE = "Enter blank to accept seat selection, or enter new seating position";

        public const string CHECK_BOOKING_OPTION_MESSAGE = "Enter booking id, or enter blank to go back to main menu :";

        public const int AVAILABLE = -1;
        public const int OCCUPIED = 1;
        public const int ALLOCATED = 0;

        public const int MAX_ROWS = 26;
        public const int MAX_SEATS_PER_ROWS = 50;

        public const string GIC = "GIC000";

        public const int ASCII_VALUE = 65;

        public const string ERROR_MESSAGE_INPUT_3PARAMETERS = "Please input 3 parameters in following format : [Title] [Row] [SeatPerRow]";
        public const string ERROR_MESSAGE_VALID_ROW_NO = "Please input valid number for [Row]";
        public const string ERROR_MESSAGE_VALID_SEATS_PER_ROW = "Please input valid number for [SeatsPerRow]";
        public const string ERROR_MESSAGE_NON_ZERO_ALLOWED_FOR_ROW_NO = "Please input non-zero number for [Row]";

        public const string ERROR_MESSAGE_MAX_NO_FOR_ROW = "Please input number from 1 to 26 for [Row]";
        public const string ERROR_MESSAGE_NON_ZERO_ALLOWED_FOR_SEATS_PER_ROW = "Please input non-zero number for [SeatPerRow]";
        public const string ERROR_MESSAGE_MAX_SEATS_PER_ROW = "Please input number from 1 to 50 for [seatsPerRow]";

        public const string ERROR_MESSAGE_VALID_INPUT_NO =  "Please input valid number";
        public const string ERROR_MESSAGE_VALID_OPTIONS = "Please provide valid option. Option could be either of 1, 2 , 3";

        public const string ERROR_MESSAGE_NON_ZERO_FOR_NO_OF_TICKETS = "Please input non - zero number for number of Tickets";

        public const string ERROR_MESSAGE_BOOKING_NOT_EXISTS = "This booking id doesnt exist" ;

        public const string ERROR_MESSAGE_VALID_SEAT = "Please input valid Seat";


        public const string ERROR_MESSAGE_NON_ZERO_FOR_SEATS = "Please input valid Seat. No Negative number allowed in Seats";

        public const string SELECTED_SEATS = "Selected seats:";

        public const string BOOKING_ID = "Booking id";

    }
}
