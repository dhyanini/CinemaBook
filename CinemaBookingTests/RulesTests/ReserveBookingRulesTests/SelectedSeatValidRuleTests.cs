using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Validations.Rules;
using Xunit;


namespace CinemaBookingCore.Validations.ReserveBookingRules
{
    public class SelectedSeatValidRuleTests
    {
        private SelectedSeatValidRule rule = new SelectedSeatValidRule(1);

        [Theory]
        [InlineData("abcd")]
        [InlineData("F100")]
        [InlineData("AB1")]

        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_VALID_SEAT);
        }

        [Theory]
        [InlineData("F10")]
        [InlineData("A1")]
        [InlineData("A01")]

        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
