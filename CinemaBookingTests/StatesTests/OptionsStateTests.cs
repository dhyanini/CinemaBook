using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class OptionsStateTests
    {
        public void SetUp( out CinemaContext cc, out State currentState)
        {
            State seatingMapState = StateFactoryCache.Instance.GetState(StateType.SeatingMap, null);
            cc = new CinemaContext(seatingMapState);
            var cinema = new CinemaBookingCore.Models.Cinema() { MovieName= "Inception", Rows=10, SeatsPerRow= 8, Available = 80};
            cc.SetCinema(cinema);
            currentState = StateFactoryCache.Instance.GetState(StateType.Options, seatingMapState);
            cc.TransitionTo(currentState);
        }

        [Theory]
        [InlineData("invalid")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged_Test1(string input)
        {
            State currentState = null;
            CinemaContext cc = null;

            SetUp(out cc, out currentState);

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<OptionsState>(cc.State);
        }

        [Theory]
        [InlineData("-1")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged_Test2(string input)
        {
            State currentState = null;
            CinemaContext cc = null;

            SetUp(out cc, out currentState);

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<OptionsState>(cc.State);
        }      
    }
}
