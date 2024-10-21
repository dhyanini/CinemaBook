using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBookingCore.Utilities
{
    public enum Options
    {
        Book = 1,
        CheckBookings = 2,
        Exit = 3
    }

    public enum StateType
    {
        NoState = 0,
        StartBooking = 1,
        CheckBooking = 2,
        Exit = 3,
        SeatingMap,
        Options,        
        ReserveBooking
    }
}
