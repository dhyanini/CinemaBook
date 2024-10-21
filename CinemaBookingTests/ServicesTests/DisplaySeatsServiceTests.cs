using CinemaBookingCore.Services;
using System;
using Xunit;

namespace CinemaBookingTests
{
    public class DisplaySeatsServiceTests
    {
        [Fact]
        public void BookSeatsInRow_WhenGoodInput()
        {

        }

        [Fact]
        public void PrintSeats()
        {
            var data1 = new int[,] { { 0, 0,0,0 }, { 0, 0,0,0 } };
            DisplaySeatsService b = DisplaySeatsService.Instance;
            b.Display(data1);
        }

      
    }
}
