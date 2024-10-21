using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class StartBookingStateTests
    {
        CinemaContext cc = null;
        State seatingMapState = null;
        State optionsState = null;

        State currentState = null;

        public void SetUp()
        {
            seatingMapState = StateFactoryCache.Instance.GetState(StateType.SeatingMap, null);
            cc = new CinemaContext(seatingMapState);
            var cinema = new CinemaBookingCore.Models.Cinema() { MovieName = "Inception", Rows = 10, SeatsPerRow = 8, Available = 80 };
            cc.SetCinema(cinema);
            cc.Cinema.SeatingMap = new int[10, 10];

            optionsState = StateFactoryCache.Instance.GetState(StateType.Options, seatingMapState);
            cc.TransitionTo(optionsState);
            currentState = StateFactoryCache.Instance.GetState(StateType.StartBooking, optionsState);
            cc.TransitionTo(currentState);
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("-20")]
        [InlineData("0")]
        [InlineData("100")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<StartBookingState>(cc.State);
        }        
    }
}
