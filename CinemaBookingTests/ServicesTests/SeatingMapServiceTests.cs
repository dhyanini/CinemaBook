using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Services;

namespace CinemaBookingTests
{
    public class SeatingMapServiceTests
    {

        [Theory]
        [InlineData(8, 10 )]        
        public void BuildSeatingMap_WhenRowAndColumnAreNonZero_ReturnValidSeatingMap(int row, int seatsPerRow)
        {
            int[,] expected = new int[8, 10] { { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE }, { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE }, { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE }, { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE }, { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE },
                                                { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE },{ AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE },{ AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE,AVAILABLE }};

            SeatingMapService sp = SeatingMapService.Instance;
            var result = sp.BuildSeatingMap(row, seatsPerRow);
            Assert.Equal(result, expected);
        }
    }
}
