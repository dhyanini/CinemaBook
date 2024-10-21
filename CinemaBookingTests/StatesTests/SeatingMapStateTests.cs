using CinemaBookingCore.Factories;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using Xunit;

namespace CinemaBookingTests.StatesTests
{
    public class SeatingMapStateTests
    {
        State currentState = null;
        CinemaContext cc = null;

        public void SetUp()
        {
            currentState = StateFactoryCache.Instance.GetState(StateType.SeatingMap, null);
            cc = new CinemaContext(currentState);
        }

        [Theory]
        [InlineData("")]
        [InlineData("OnlyOneArgument")]
        [InlineData("FirstArgument  SecondArgument")]
        [InlineData("Inception  abc 20")]
        [InlineData("Inception  10 abc")]
        [InlineData("Inception  -10 20")]
        [InlineData("Inception  0 20")]
        [InlineData("Inception  20 -10 ")]
        [InlineData("Inception  20 0 ")]
        [InlineData("Inception  50 20")]
        [InlineData("Inception  5 51")]
        public void HandleTest_WhenInvalidInputPassed_StateNotChanged(string input)
        {
            SetUp();

            var result = currentState.Handle(input);

            Assert.True(result);
            Assert.IsType<SeatingMapState>(cc.State);
        }
    }
}
