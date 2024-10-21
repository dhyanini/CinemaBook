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
    public class DefaultSeatsSelectionTests
    {
        private DefaultRowSeatsSelectorService dfs = new DefaultRowSeatsSelectorService();

        /// Empty Seats

        //Row per seats are Odd OR Even => doesnt matter
        [Theory]
        [InlineData(10  , new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE }, 
                          new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED } )]
        // Input : Sample Visualization Of Available Seats in Row :=>   .  .   .   .   .   .   .   .   .   .
        //Output Should be : =>    O    O   O   O   O   O   O   O   O   O 
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsEqualToRowSeats_ReturnsAllSeatsInCurrentRow
            (int seatsToBook, int[] rowSeats, int[] expected)
        {
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Row per seats are Odd OR Even => doesnt matter
        [Theory]
        [InlineData(13, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE }, 
                        new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .  .   .   .   .   .   .   .   .   .
        //Output Should be : =>    O    O   O   O   O   O   O   O   O   O 
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsMoreThanRowSeats_ReturnsAllSeatsInCurrentRowAndOverflowToNextRow
            (int seatsToBook, int[] rowSeats, int[] expected)
        {
            var result =  dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Required Seats = Odd
        [Theory]
        [InlineData(3, new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .   .   .   .   .   .   .   .   .
        //Output Should be : =>   .   .   .   O   O   O   .   .   .
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsIsOddAndLessThanRowSeatsAndSeatsPerRowIsOdd_ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Required Seats = Even
        [Theory]
        [InlineData(4, new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .   .   .   .   .   .   .   .   .
        //Output Should be : =>   .   .   O   O   O   O   .   .   .         OR      .   .   .   O   O   O   O   .   .  
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsIsEvenAndLessThanRowSeatsAndSeatsPerRowIsOdd_ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }


        //Even Seats Per Row
        //Required Seats = Odd
        [Theory]
        [InlineData(3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .   .   .   .   .   .   .   .   .   .
        //Output Should be : =>  .   .   .   ALLOCATED   ALLOCATED   ALLOCATED   .   .   .   .      OR      .   .   .   .   ALLOCATED   ALLOCATED   ALLOCATED   .   .   . 
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsIsOddAndLessThanRowSeatsAndSeatsPerRowIsEven_ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(4, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
        //Even Seats Per Row
        //Required Seats = Even
        // Input : Sample Visualization Of Available Seats in Row :=>   .   .   .   .   .   .   .   .   .   .
        //Output Should be : =>  .  .   .   ALLOCATED   ALLOCATED   ALLOCATED   ALLOCATED   .   .   .
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsEmptyAndRequiredSeatsIsEvenAndLessThanRowSeatsAndSeatsPerRowIsEven_ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }


        /// Non Empty Seats

        //Row per seats are Odd OR Even => doesnt matter
        [Theory]
        [InlineData(10, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                        new int[10] { ALLOCATED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, ALLOCATED, ALLOCATED })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .  .   .   #  #   #   .   .   .   .
        //Output Should be : =>    ALLOCATED    ALLOCATED   ALLOCATED   #   #   #   ALLOCATED   ALLOCATED   ALLOCATED   ALLOCATED 
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsEqualToTotalRowSeats_ReturnsAllSeatsInCurrentRow
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Row per seats are Odd OR Even => doesnt matter
        [Theory]
        [InlineData(13, new int[10] { OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE },
                        new int[10] { OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, ALLOCATED, ALLOCATED })]
        // Input : Sample Visualization Of Available Seats in Row :=>   #  .   .   #   #   .   .   #   .   .
        //Output Should be : =>    #    ALLOCATED   ALLOCATED   #   #   ALLOCATED   ALLOCATED   #   ALLOCATED   ALLOCATED 
        public void GetDefaultSeatsSelection_ForSingleRowWhenRowIsNonEmptyAndRequiredSeatsMoreThanTotalRowSeats_ReturnsAllSeatsInCurrentRowAndOverflowToNextRow
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        /////*********************** SEATS PER ROW = ODD *******************************
        
        //Odd  Seats Per Row
        //Occupied Seats = Middle OCCUPIED
        [Theory]
        [InlineData(3, new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, OCCUPIED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .  .   .   .   #   .   .   .   .   
        //Output Should be : =>  .  .  O   O   #   O   .   .    .    OR       .  .  .   O   #   O   O   .    .    
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatIsOCCUPIEDMiddle__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Occupied Seats = Middle 3
        [Theory]
        [InlineData(3, new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[9] { AVAILABLE, AVAILABLE, ALLOCATED, OCCUPIED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   .  .   .   #   #   #   .   .   .   
        //Output Should be : =>     .  O   O   #   #   #   O   .   .     OR         .  .   O   #   #   #   O   O   . 
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatIs3Middle__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Occupied Seats = Left Half
        [Theory]
        [InlineData(3, new int[9] { OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[9] { OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #   #   #  #   #   .   .   .   . 
        //Output Should be : =>     #   #   #   #   #   O   O   O   .
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatIsLeftHalf__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Occupied Seats = Right Half
        [Theory]
        [InlineData(3, new int[9] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED },
                       new int[9] { AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED })]
        // Sample Visualization Of Available Seats :=>   .   .   .   .  #   #   #  #   # 
        //Output Should be : =>     .   O   O   O  #   #   #  #   #
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatIsRightHalf__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Occupied Seats =  Randomly Distributed
        [Theory]
        [InlineData(3, new int[9] { OCCUPIED, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE },
                       new int[9] { OCCUPIED, AVAILABLE, OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #   .   #   .  .   #   #  .   .
        //Output Should be : =>     #   ALLOCATED   #   O   O   #   #   .   .   OR     #   .   #   O   O   #   #   O   .
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatAreRandomlyDistributed__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Odd  Seats Per Row
        //Occupied Seats =  Randomly Distributed
        [Theory]
        [InlineData(3, new int[9] { OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE },
                       new int[9] { OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #   .   #   #  .   #   #  .   .
        //Output Should be : =>     #   O   #   #   O   #   #   O   . 
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsOdd_AndOccupiedSeatAreRandomlyDistributed2__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }


        /////*********************** SEATS PER ROW = EVEN *******************************

        //Even  Seats Per Row
        //Occupied Seats = Middle OCCUPIED
        [Theory]
        [InlineData(3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, OCCUPIED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE, AVAILABLE })]
        // Input : Sample Visualization Of Available Seats in Row :=>   .  .   .   .   #   .   .   .   .    .  
        //Output Should be : =>  .  .  .   O   #   O   O   .    .   . 
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatIsOCCUPIEDMiddle__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Even  Seats Per Row
        //Occupied Seats = Middle 2
        [Theory]
        [InlineData(3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   .  .   .   .   #   #   .   .   .   .
        //Output Should be : =>     .  .   O   O   #   #   O   .   .    .    OR         .  .   .   O   #   #   O   O   .   .
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatIs2Middle__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Even  Seats Per Row
        //Occupied Seats = Left Half
        [Theory]
        [InlineData(3, new int[10] { OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE },
                       new int[10] { OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, ALLOCATED, ALLOCATED, ALLOCATED, AVAILABLE, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #   #   #  #   #   .   .   .   .   .
        //Output Should be : =>     #   #   #   #   #   O   O   O   .   .
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatIsLeftHalf__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Even  Seats Per Row
        //Occupied Seats = Right Half
        [Theory]
        [InlineData(3, new int[10] { AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED },
                       new int[10] { AVAILABLE, AVAILABLE, ALLOCATED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED, OCCUPIED })]
        // Sample Visualization Of Available Seats :=>   .  .   .   .   .  #   #   #  #   # 
        //Output Should be : =>     .   .   O   O   O  #   #   #  #   #
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatIsRightHalf__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Even  Seats Per Row
        //Occupied Seats =  Randomly Distributed
        [Theory]
        [InlineData(3, new int[10] { OCCUPIED, AVAILABLE, OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, OCCUPIED, AVAILABLE },
                       new int[10] { OCCUPIED, AVAILABLE, OCCUPIED, ALLOCATED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, OCCUPIED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #   .   #   .  .   #   #  .   #   .
        //Output Should be : =>     #   .   #   O   O   #   #   O   #   . 
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatAreRandomlyDistributed__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }

        //Even  Seats Per Row
        //Occupied Seats =  Randomly Distributed
        [Theory]
        [InlineData(3, new int[10] { OCCUPIED, AVAILABLE, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, OCCUPIED, OCCUPIED, AVAILABLE, AVAILABLE },
                       new int[10] { OCCUPIED, AVAILABLE, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, OCCUPIED, OCCUPIED, ALLOCATED, AVAILABLE })]
        // Sample Visualization Of Available Seats :=>   #  .   .   #   #  .   #   #  .   .
        //Output Should be : =>    #    .   O   #   #   O   #   #   O   .       OR     #    O   O   #   #   O   #   #   .   .      
        public void GetDefaultSeatsSelection__ForSingleRow_WhenRowIsNonEmpty_AndRequiredSeatsIsLessThanAvailableRowSeats_AndSeatsPerRowIsEven_AndOccupiedSeatAreRandomlyDistributed2__ReturnsMiddleMostPossibleSeats
            (int seatsToBook, int[] rowSeats, int[] expected)
        {            
            var result = dfs.GetDefaultSeats(seatsToBook, rowSeats);
            Assert.Equal(result, expected);
        }
    }
}
