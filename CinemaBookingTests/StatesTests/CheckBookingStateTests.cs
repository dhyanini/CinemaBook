using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System.Collections.Generic;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class CheckBookingStateTests
    {
        CinemaContext cc = null;
        State seatingMapState = null;
        State optionsState = null;

        State currentState = null;

        [Fact]
        public void SetUp()
        {
            seatingMapState = StateFactoryCache.Instance.GetState(StateType.SeatingMap, null);
            cc = new CinemaContext(seatingMapState);
            var cinema = new CinemaBookingCore.Models.Cinema() { MovieName = "Inception", Rows = 10, SeatsPerRow = 8, Available = 80 };
            cc.SetCinema(cinema);
            cc.Cinema.SeatingMap = new int[10, 10];
            cc.BookingIdToSeatingMap["GIC0001"] = new List<CinemaBookingCore.Models.Booking>();

            optionsState = StateFactoryCache.Instance.GetState(StateType.Options, seatingMapState);
            cc.TransitionTo(optionsState);
            currentState = StateFactoryCache.Instance.GetState(StateType.CheckBooking, optionsState);
            cc.TransitionTo(currentState);
        }

        [Theory]
        [InlineData("GIC0002")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<CheckBookingState>(cc.State);
        }

        [Theory]
        [InlineData("GIC0001")]
        public void HandleTest_WhenValidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<CheckBookingState>(cc.State);
        }

        [Theory]
        [InlineData("")]
        public void HandleTest_WhenValidInputPassed_StateChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsNotType<CheckBookingState>(cc.State);
        }
    }
}
