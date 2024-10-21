using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System.Collections.Generic;
using Xunit;


namespace CinemaBookingTests.StatesTests
{
    public class ReserveBookingStateTests
    {
        CinemaContext cc = null;
        State seatingMapState = null;
        State optionsState = null;
        State startBookingState = null;

        State currentState = null;

        [Fact]
        public void SetUp()
        {
            seatingMapState = StateFactoryCache.Instance.GetState(StateType.SeatingMap, null);
            cc = new CinemaContext(seatingMapState);
            var cinema = new CinemaBookingCore.Models.Cinema() { MovieName = "Inception", Rows = 10, SeatsPerRow = 8, Available = 80 };
            cc.SetCinema(cinema);
            cc.Cinema.SeatingMap = new int[10, 10];

            optionsState = StateFactoryCache.Instance.GetState(StateType.Options, seatingMapState);
            cc.TransitionTo(optionsState);
            startBookingState = StateFactoryCache.Instance.GetState(StateType.StartBooking, optionsState);
            cc.TransitionTo(startBookingState);

            currentState = StateFactoryCache.Instance.GetState(StateType.ReserveBooking, startBookingState);
            cc.TransitionTo(currentState);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("@13")]
        [InlineData("abcd")]
        [InlineData("F100")]
        [InlineData("AB1")]
        [InlineData("F11")]
        [InlineData("F0")]
        [InlineData("F-5")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<ReserveBookingState>(cc.State);
        }

        [Theory]
        [InlineData("F10")]
        [InlineData("f10")]
        [InlineData("A1")]
        [InlineData("a01")]
        public void HandleTest_WhenValidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<ReserveBookingState>(cc.State);
        }

        [Theory]
        [InlineData("")]
        public void HandleTest_WhenValidInputPassed_StateChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsNotType<ReserveBookingState>(cc.State);
        }
    }
}
