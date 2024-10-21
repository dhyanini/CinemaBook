using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Validations.Rules;
using Xunit;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;

namespace CinemaBookingCore.Validations.ReserveBookingRules
{
    public class SelectedSeatAvailabilityRuleTests
    {
        private SelectedSeatAvailabilityRule rule = new SelectedSeatAvailabilityRule(1);
        private CinemaContext cinemaContext = new CinemaContext();

        public SelectedSeatAvailabilityRuleTests()
        {
            cinemaContext.SetCinema(new Models.Cinema { SeatsPerRow = 10 });
        }

        [Theory]
        [InlineData("F11")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input, cinemaContext);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("F10")]
        [InlineData("f10")]
        [InlineData("A1")]
        [InlineData("a01")]

        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
