using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class StateFactoryCacheTests
    {
        StateFactoryCache stateFactoryCache = StateFactoryCache.Instance;

        [Theory]
        [InlineData(StateType.SeatingMap)]

        public void GetStateTests_WhenCurrentStateIsNull_ReturnSeatingMapState(StateType nextStateType)
        {
            State currentState = null;
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<SeatingMapState>(nextState);
        }

        [Theory]
        [InlineData(StateType.Options)]

        public void GetStateTests_WhenCurrentStateIsSeatingMap_ReturnOptionsState(StateType nextStateType)
        {
            State currentState = new SeatingMapState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<OptionsState>(nextState);
        }

        [Theory]
        [InlineData(StateType.StartBooking)]

        public void GetStateTests_WhenCurrentStateIsOptions_ReturnStartBookingState(StateType nextStateType)
        {
            State currentState = new OptionsState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<StartBookingState>(nextState);
        }

        [Theory]
        [InlineData(StateType.CheckBooking)]

        public void GetStateTests_WhenCurrentStateIsOptions_ReturnCheckBookingState(StateType nextStateType)
        {
            State currentState = new OptionsState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<CheckBookingState>(nextState);
        }

        [Theory]
        [InlineData(StateType.Exit)]

        public void GetStateTests_WhenCurrentStateIsOptions_ReturnExitState(StateType nextStateType)
        {
            State currentState = new OptionsState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<ExitState>(nextState);
        }

        [Theory]
        [InlineData(StateType.ReserveBooking)]
        public void GetStateTests_WhenCurrentStateIsStartBookingState_ReturnReserveBookingState(StateType nextStateType)
        {
            State currentState = new StartBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<ReserveBookingState>(nextState);
        }

        [Theory]
        [InlineData(StateType.Options)]
        public void GetStateTests_WhenCurrentStateIsStartBookingState_ReturnOptionsState(StateType nextStateType)
        {
            State currentState = new StartBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<OptionsState>(nextState);
        }

        [Theory]
        [InlineData(StateType.CheckBooking)]
        public void GetStateTests_WhenCurrentStateIsCheckBookingState_ReturnCheckBookingState(StateType nextStateType)
        {
            State currentState = new CheckBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<CheckBookingState>(nextState);
        }

        [Theory]
        [InlineData(StateType.Options)]
        public void GetStateTests_WhenCurrentStateIsCheckBookingState_ReturnOptionsState(StateType nextStateType)
        {
            State currentState = new CheckBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<OptionsState>(nextState);
        }

        [Theory]
        [InlineData(StateType.ReserveBooking)]
        public void GetStateTests_WhenCurrentStateIsReserveBookingState_ReturnReserveBookingState(StateType nextStateType)
        {
            State currentState = new ReserveBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<ReserveBookingState>(nextState);
        }

        [Theory]
        [InlineData(StateType.Options)]
        public void GetStateTests_WhenCurrentStateIsReserveBookingState_ReturnOptionsState(StateType nextStateType)
        {
            State currentState = new ReserveBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<OptionsState>(nextState);
        }


        [Theory]
        [InlineData(StateType.StartBooking)]
        public void GetStateTests_WhenInvalidInputProvided_ReturnCurrentState(StateType nextStateType)
        {
            State currentState = new ReserveBookingState(null);
            var nextState = stateFactoryCache.GetState(nextStateType, currentState);
            Assert.IsType<ReserveBookingState>(nextState);
        }
    }
}
