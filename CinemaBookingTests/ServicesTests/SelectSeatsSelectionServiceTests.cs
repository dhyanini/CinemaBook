using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CinemaBookingCore;
using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Services;

namespace CinemaBookingTests
{
    public class SelectSeatsSelectionServiceTests
    {
        //When Whole Row is Empty
        [Theory]
        [InlineData(0, 10, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE }, 
                           new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsEmptyAndRequiredSeatsEqualToRowSeats__ReturnsAllSeatsInCurrentRow(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(0, 13, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                           new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsEmptyAndRequiredSeatsMoreThanRowSeats__ReturnsAllSeatsInCurrentRowAndOverflowToNextRow(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }


        [Theory]
        [InlineData(0, 3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                          new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsEmptyAndRequiredSeatsLessThanRowSeats__ReturnsSeatsFromStart(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(4, 3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE }, 
                          new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsEmptyAndRequiredSeatsLessThanRowSeats__ReturnsSeatsFromMiddle(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //NON EMPTY SEATS

        [Theory]
        [InlineData(0, 10, new int[10] { OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE },
                           new int[10] { OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, ALLOCATED, OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, ALLOCATED })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsEqualToRowSeats__ReturnsRemainingSeatsInCurrentRow(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(0, 13, new int[10] { OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE }, 
                           new int[10] { OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, ALLOCATED, OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, ALLOCATED })]
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsMoreThanRowSeats__ReturnsRemainingSeatsInCurrentRowAndOverflowToNextRow(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }


        [Theory]
        [InlineData(0, 3, new int[10] { OCCUPIED, AVAILABLE, OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE }, 
                          new int[10] { OCCUPIED, ALLOCATED, OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
       
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsLessThanRowSeats__ReturnsSeatsFromStart(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(4, 3, new int[10] { AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE }, 
                          new int[10] { AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED })]
        
        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsLessThanRowSeats__ReturnsSeatsFromSelectedIndex(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(3, 10, new int[10] { AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE },
                           new int[10] { AVAILABLE, OCCUPIED, AVAILABLE, ALLOCATED, OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED })]

        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsMoreThanRowSeats__ReturnsRemainingSeatsFromSelectedIndex(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(9, 3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                          new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED })]

        public void GetSelectSeatsSelection__ForSingleRowWhenRowIsNonEmpty__ReturnsEndSeat(int colIndex, int seatsToBook, int[] rowSeats, int[] expected)
        {
            var sss = new SelectedRowSeatsSelectorService();
            var result = sss.GetSeats(colIndex, seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }
    }
}
