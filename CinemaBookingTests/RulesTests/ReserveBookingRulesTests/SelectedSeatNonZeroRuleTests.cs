using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Validations.Rules;
using Xunit;

namespace CinemaBookingCore.Validations.ReserveBookingRules
{
    public  class SelectedSeatNonZeroRuleTests
    {
        private SelectedSeatNonZeroRule rule = new SelectedSeatNonZeroRule(1);

        [Theory]
        [InlineData("F0")]
        [InlineData("F-5")]
        public void Evaluate_WhenInvalidRowNoPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_NON_ZERO_FOR_SEATS);
        }

        [Theory]
        [InlineData("F10")]
        public void Evaluate_WhenValidRowNoPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
