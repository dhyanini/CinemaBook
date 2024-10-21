using CinemaBookingCore.Models;
using CinemaBookingCore.Services;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests
{
    public class HelperTests
    {
        [Fact]
        public void GetBookingMessageTest()
        {
            Cinema cinema = new Cinema() { MovieName  = "Ïnception", Available  = 100};
            var bookingMsg =  Helper.GetBookingMessage(cinema);
            Assert.Equal($"[1] Book Tickets for { cinema.MovieName } ({cinema.Available} seats available)", bookingMsg);
        }

        [Fact]
        public void  GetBookingReservedMessage()
        {
            Cinema cinema = new Cinema() { MovieName = "Ïnception", Available = 100 };
            var bookingReserveMsg = Helper.GetBookingReservedMessage(10, cinema);
            Assert.Equal($"Successfully reserved {10} { cinema.MovieName } tickets", bookingReserveMsg);
        }


        [Fact]
        public void ProcessStringTest()
        {
            var result = Helper.ProcessString("Inception 10 8");
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void CheckIsNumberTest()
        {
            var result = Helper.CheckIsNumber("7");
            Assert.True(result);
        }

        [Theory]
        [InlineData(10)]        
        public void BuildRowNameToRowNoMapTest(int rows)
        {
            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            var result = Helper.BuildRowNameToRowNoMap(rows);

            Assert.Equal(rowNameToRowNoMap, result);
        }

        [Fact]
        public void BuildBookingsTest()
        {
            int[,] seatsAllocated = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            seatsAllocated[5, 5] = ALLOCATED;
            CinemaContext cinemaContext = new CinemaContext();
            Cinema cinema = new Cinema() { MovieName = "Ïnception", Available = 100 };
            cinemaContext.SetCinema(cinema);
            cinemaContext.Cinema.SeatingMap = SeatingMapService.Instance.BuildSeatingMap(10, 10);


            int[,] expected = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            expected[5, 5] = OCCUPIED;

            List<Booking> bookings = new List<Booking>();
            bookings.Add(new Booking() { Row = 5, Col = 5 });

            var result = Helper.BuildBookings(seatsAllocated, cinemaContext);
            Assert.Equal(bookings[0].Row, result[0].Row);
            Assert.Equal(bookings[0].Col, result[0].Col);
        }

        [Fact]
        public void GetBookingsTest()
        {
            //Arrange
            var bookingId = "GIC0001";
            CinemaContext cinemaContext = new CinemaContext();
            Cinema cinema = new Cinema() { MovieName = "Ïnception", Available = 100 };
            cinemaContext.SetCinema(cinema);
            cinemaContext.Cinema.SeatingMap =  SeatingMapService.Instance.BuildSeatingMap(10, 10);
            cinemaContext.BookingIdToSeatingMap[bookingId] = new List<Booking>() { };
            cinemaContext.BookingIdToSeatingMap[bookingId].Add(new Booking() { Row = 5, Col = 5 });
            int[,] expected = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            expected[5, 5] = ALLOCATED;

            //Act
            int[,] result = Helper.GetBookings(bookingId, cinemaContext);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
