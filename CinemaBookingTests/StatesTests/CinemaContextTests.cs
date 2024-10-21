using CinemaBookingCore.Models;
using CinemaBookingCore.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class CinemaContextTests
    {
        CinemaContext cinemaContext = new CinemaContext();

        [Fact]        
        public void IncreamentBookingTest()
        {
            cinemaContext.IncrementBookingCount();
            Assert.Equal(2, cinemaContext.BookingCount);
        }

        [Theory]
        [InlineData(10)]
        public void SetTotalSeatsToBookTest(int totalSeatsToBook)
        {
            cinemaContext.SetTotalSeatsToBook(totalSeatsToBook);
            Assert.Equal(totalSeatsToBook, cinemaContext.TotalSeatsToBook);
        }

        [Theory]
        [InlineData(20, 10)]
        public void SetSelectedRowTest(int totalRows, int selectedRow)
        {
            cinemaContext.SetCinema(new Cinema());
            cinemaContext.Cinema.SeatingMap = new int[totalRows, 10];
            cinemaContext.SetSelectedRow(selectedRow);
            Assert.Equal(totalRows -1 - selectedRow, cinemaContext.SelectedRow);
        }

        [Theory]
        [InlineData(5)]
        public void SetSelectedColTest(int selectedCol)
        {
            cinemaContext.SetSelectedCol(selectedCol);
            Assert.Equal(selectedCol-1, cinemaContext.SelectedCol);
        }

        [Theory]
        [InlineData("GIC0001")]
        public void AddBookingIdTest(string key)
        {
            List<Booking> bookings = new List<Booking>();
            cinemaContext.AddBookingId(key, bookings);
            Assert.Equal(bookings, cinemaContext.BookingIdToSeatingMap[key]);
        }

        [Theory]
        [InlineData(10)]
        public void UpdateSeatAvailabilityTest(int totalSeatsBooked)
        {
            cinemaContext.SetCinema(new Cinema());
            cinemaContext.UpdateSeatAvailability(totalSeatsBooked);
            Assert.Equal(- totalSeatsBooked, cinemaContext.Cinema.Available);
        }

        [Fact]
        public void  SetCinemaTest()
        {
            var cinema = new Cinema();
            cinemaContext.SetCinema(cinema);
            Assert.Equal(cinema, cinemaContext.Cinema);
        }
    }
}
