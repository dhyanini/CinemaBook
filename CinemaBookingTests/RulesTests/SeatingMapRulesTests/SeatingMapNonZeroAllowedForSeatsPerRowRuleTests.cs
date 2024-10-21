using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.RulesTests
{
    public class SeatingMapNonZeroAllowedForSeatsPerRowRuleTests
    {

        private SeatingMapNonZeroAllowedForSeatsPerRowRule rule = new SeatingMapNonZeroAllowedForSeatsPerRowRule(1);

        [Theory]
        [InlineData("Inception  20 -10 ")]
        [InlineData("Inception  20 0 ")]
        public void Evaluate_WhenInvalidSeatsPerRowPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_NON_ZERO_ALLOWED_FOR_SEATS_PER_ROW);
        }

        [Theory]
        [InlineData("Inception  10  20")]
        public void Evaluate_WhenValidSeatsPerRowPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
