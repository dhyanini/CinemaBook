using CinemaBookingCore.Models;
using System.Collections.Generic;

namespace CinemaBookingCore.States
{

    public class CinemaContext 
    {
        public State State { get; private set; }  = null;
      
        public virtual Cinema Cinema { get; private set; } = null;
        public int BookingCount { get; private set; } = 1;

        public Dictionary<char, int> RowNameToRowNoMap { get; set; }  = new Dictionary<char, int>();

        public int Rows => Cinema.SeatingMap.GetLength(0);
        public int Cols => Cinema.SeatingMap.GetLength(1);

        public Dictionary<string, List<Booking>> BookingIdToSeatingMap { get; private set; } = new Dictionary<string, List<Booking>>();

        public int TotalSeatsToBook { get; private set; }
        public int SelectedRow { get; private set; }
        public int? SelectedCol { get; private set; }

        public CinemaContext()
        {

        }
        public CinemaContext(State state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
            State = state;
            State.SetContext(this);
        }

        public bool Handle(string input)
        {
            return State.Handle(input);
        }

        public void SetCinema( Cinema cinema)
        {
            Cinema = cinema;
        }
        public void IncrementBookingCount()
        {
            BookingCount++;
        }

        public void SetTotalSeatsToBook(int totalSeatsToBook)
        {
            TotalSeatsToBook = totalSeatsToBook;
        }

        public void SetSelectedRow(int selectedRow)
        {
            SelectedRow = Rows - 1 - selectedRow;
        }

        public void SetSelectedCol(int? selectedCol)
        {
            SelectedCol = selectedCol - 1;
        }

        public void AddBookingId(string key, List<Booking> bookings)
        {
            BookingIdToSeatingMap[key] = bookings;
        }

        public void UpdateSeatAvailability(int totalSeatsBooked)
        {
            Cinema.Available = Cinema.Available - totalSeatsBooked;
        }
    }
}
