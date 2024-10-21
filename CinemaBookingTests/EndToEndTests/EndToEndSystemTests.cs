using CinemaBookingCore.Factories;
using CinemaBookingCore.Models;
using CinemaBookingCore.Services;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.EndToEndTests
{    public class EndToEndSystemTests
    {
        [Theory ]
        [InlineData("Inception 10 10", "1", "5", "", "3")] 
        //Movie Name , Rows, SeatsPerRow =>  Option 1 is Book =>  Now of seats to book = 5 =>  Accept Booking => Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenOnlyOneBookingDoneAndThenExited(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse (cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            var seatsToBook = int.Parse(inputs[2]);

            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            List<Tuple<int, int>> rowAndCols = new List<Tuple<int, int>>();
            rowAndCols.Add(new Tuple<int, int>(9, 3));
            rowAndCols.Add(new Tuple<int, int>(9, 4));
            rowAndCols.Add(new Tuple<int, int>(9, 5));
            rowAndCols.Add(new Tuple<int, int>(9, 6));
            rowAndCols.Add(new Tuple<int, int>(9, 7));


            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            rowAndCols.ForEach(rowAndCol => expectedSeatMapServiceAfterBookingDone[rowAndCol.Item1, rowAndCol.Item2] = OCCUPIED);

            var bookingId = $"{GIC}1";
            List<Booking> expectedBookings = new List<Booking>();

            rowAndCols.ForEach(rowAndCol => expectedBookings.Add(new Booking() { Row = rowAndCol.Item1, Col = rowAndCol.Item2 }));

            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(seatsToBook, cc.TotalSeatsToBook);
            Assert.Equal(totalSeats- seatsToBook, cc.Cinema.Available);
            Assert.Equal(2, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.Equal(expectedBookings, cc.BookingIdToSeatingMap[bookingId]);
            Assert.IsType<ExitState>(cc.State);
        }

        [Theory]
        [InlineData("Inception 10 10", "1", "5", "", "1", "3", "", "3")]
        //Movie Name , Rows, SeatsPerRow =>  Option 1 is Book =>  Now of seats to book = 5 =>  Accept Booking =>
        // Option 1 is Book =>  Now of seats to book = 3 =>  Accept Booking => Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenTwoBookingsDoneAndThenExited(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse(cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            var seatsToBookFor1stBooking = int.Parse(inputs[2]);
            var seatsToBookFor2ndBooking = int.Parse(inputs[5]);
            var totalSeatsToBook = seatsToBookFor1stBooking + seatsToBookFor2ndBooking;

            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            List<Tuple<int, int>> rowAndColsForFirstBooking = new List<Tuple<int, int>>();
            rowAndColsForFirstBooking.Add(new Tuple<int, int>(9, 3));
            rowAndColsForFirstBooking.Add(new Tuple<int, int>(9, 4));
            rowAndColsForFirstBooking.Add(new Tuple<int, int>(9, 5));
            rowAndColsForFirstBooking.Add(new Tuple<int, int>(9, 6));
            rowAndColsForFirstBooking.Add(new Tuple<int, int>(9, 7));


            List<Tuple<int, int>> rowAndColsForSecondBooking = new List<Tuple<int, int>>();
            rowAndColsForSecondBooking.Add(new Tuple<int, int>(9, 1));
            rowAndColsForSecondBooking.Add(new Tuple<int, int>(9, 2));
            rowAndColsForSecondBooking.Add(new Tuple<int, int>(9, 8));

            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            rowAndColsForFirstBooking.ForEach(rowAndCol => expectedSeatMapServiceAfterBookingDone[rowAndCol.Item1, rowAndCol.Item2] = OCCUPIED);
            rowAndColsForSecondBooking.ForEach(rowAndCol => expectedSeatMapServiceAfterBookingDone[rowAndCol.Item1, rowAndCol.Item2] = OCCUPIED);


            var bookingIdForFirst = $"{GIC}1";
            List<Booking> expectedBookingsForFirst = new List<Booking>();
            rowAndColsForFirstBooking.ForEach(rowAndCol => expectedBookingsForFirst.Add(new Booking() { Row = rowAndCol.Item1, Col = rowAndCol.Item2 }));

            var bookingIdForSecond = $"{GIC}2";
            List<Booking> expectedBookingsForSecond = new List<Booking>();
            rowAndColsForSecondBooking.ForEach(rowAndCol => expectedBookingsForSecond.Add(new Booking() { Row = rowAndCol.Item1, Col = rowAndCol.Item2 }));

            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(totalSeats - totalSeatsToBook, cc.Cinema.Available);
            Assert.Equal(3, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.Equal(expectedBookingsForFirst, cc.BookingIdToSeatingMap[bookingIdForFirst]);
            Assert.Equal(expectedBookingsForSecond, cc.BookingIdToSeatingMap[bookingIdForSecond]);
            Assert.IsType<ExitState>(cc.State);
        }

        [Theory]
        [InlineData("Inception 10 10", "1", "5","D8", "", "3")]
        //Movie Name , Rows, SeatsPerRow =>  Option 1 is Book =>  Now of seats to book = 5 => Change seats to D5 =>  Accept Booking => Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenOnlyOneBookingDoneAndChangeSeatsByUserAndThenExited(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse(cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            var seatsToBook = int.Parse(inputs[2]);
            var selectedSeat = inputs[3];


            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            List<Tuple<int, int>> rowAndCols = new List<Tuple<int, int>>();
            rowAndCols.Add(new Tuple<int, int>(5, 4));
            rowAndCols.Add(new Tuple<int, int>(5, 5));
            rowAndCols.Add(new Tuple<int, int>(6, 7));
            rowAndCols.Add(new Tuple<int, int>(6, 8));
            rowAndCols.Add(new Tuple<int, int>(6, 9));
        


            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            rowAndCols.ForEach(rowAndCol => expectedSeatMapServiceAfterBookingDone[rowAndCol.Item1, rowAndCol.Item2] = OCCUPIED);

            var bookingId = $"{GIC}1";
            List<Booking> expectedBookings = new List<Booking>();

            rowAndCols.ForEach(rowAndCol => expectedBookings.Add(new Booking() { Row = rowAndCol.Item1, Col = rowAndCol.Item2 }));

            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(seatsToBook, cc.TotalSeatsToBook);
            Assert.Equal(totalSeats - seatsToBook, cc.Cinema.Available);
            Assert.Equal(2, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.Equal(expectedBookings, cc.BookingIdToSeatingMap[bookingId]);
            Assert.IsType<ExitState>(cc.State);
        }


        [Theory]
        [InlineData("Inception 10 10", "1", "5", "D8", "E7","", "3")]
        //Movie Name , Rows, SeatsPerRow =>  Option 1 is Book =>  Now of seats to book = 5 => Change seats to D5 =>
        //=> Change seats to E7 => Accept Booking => Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenOnlyOneBookingDoneAndChangeSeatsByUserTwiceAndThenExited(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse(cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            var seatsToBook = int.Parse(inputs[2]);
            var selectedSeat = inputs[3];


            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            List<Tuple<int, int>> rowAndCols = new List<Tuple<int, int>>();
            rowAndCols.Add(new Tuple<int, int>(4, 5));
            rowAndCols.Add(new Tuple<int, int>(5, 6));
            rowAndCols.Add(new Tuple<int, int>(5, 7));
            rowAndCols.Add(new Tuple<int, int>(5, 8));
            rowAndCols.Add(new Tuple<int, int>(5, 9));


            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            rowAndCols.ForEach(rowAndCol => expectedSeatMapServiceAfterBookingDone[rowAndCol.Item1, rowAndCol.Item2] = OCCUPIED);

            var bookingId = $"{GIC}1";
            List<Booking> expectedBookings = new List<Booking>();

            rowAndCols.ForEach(rowAndCol => expectedBookings.Add(new Booking() { Row = rowAndCol.Item1, Col = rowAndCol.Item2 }));

            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(seatsToBook, cc.TotalSeatsToBook);
            Assert.Equal(totalSeats - seatsToBook, cc.Cinema.Available);
            Assert.Equal(2, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.Equal(expectedBookings, cc.BookingIdToSeatingMap[bookingId]);
            Assert.IsType<ExitState>(cc.State);
        }

        [Theory]
        [InlineData("Inception 10 10", "2", "GIC0001",  "", "3")]
        //Movie Name , Rows, SeatsPerRow =>  Option 2 is CheckBooking => Enter Booking Id as GIC0001  => enter to go back => Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenCheckBookingDoneByUserAndThenExited(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse(cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(0, cc.TotalSeatsToBook);
            Assert.Equal(totalSeats , cc.Cinema.Available);
            Assert.Equal(1, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.IsType<ExitState>(cc.State);
        }

        [Theory]
        [InlineData("Inception 10 10", "3")]
        //Movie Name , Rows, SeatsPerRow =>  Option 3 is Exit Booking       
        public void EndToEndSystemTest_WhenExitOptionSelectedByUser(params string[] inputs)
        {
            //Arrange
            var cinemaInputs = Helper.ProcessString(inputs[0]);
            var rows = int.Parse(cinemaInputs[1]);
            var cols = int.Parse(cinemaInputs[2]);
            var totalSeats = rows * cols;

            Dictionary<char, int> rowNameToRowNoMap = new Dictionary<char, int>();
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                rowNameToRowNoMap.Add((char)(ASCII_VALUE + rowIndex), rowIndex);
            }

            int[,] expectedSeatMapServiceAfterBookingDone = SeatingMapService.Instance.BuildSeatingMap(10, 10);
            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));

            //Act
            foreach (var input in inputs)
            {
                bool keepRunning = cc.Handle(input);
                if (!keepRunning)
                    break;
            }

            //Assert
            Assert.Equal(rowNameToRowNoMap, cc.RowNameToRowNoMap);
            Assert.Equal(cinemaInputs[0], cc.Cinema.MovieName);
            Assert.Equal(cols, cc.Cinema.SeatsPerRow);
            Assert.Equal(rows, cc.Cinema.Rows);
            Assert.Equal(rows, cc.Rows);
            Assert.Equal(cols, cc.Cols);
            Assert.Equal(0, cc.TotalSeatsToBook);
            Assert.Equal(totalSeats, cc.Cinema.Available);
            Assert.Equal(1, cc.BookingCount);
            Assert.Equal(expectedSeatMapServiceAfterBookingDone, cc.Cinema.SeatingMap);
            Assert.IsType<ExitState>(cc.State);
        }
    }
}
