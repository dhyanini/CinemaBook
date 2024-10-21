using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.RulesTests.StartBookingRulesTests
{
    public class TicketsNonZeroRuleTests
    {
        private TicketsNonZeroRule rule = new TicketsNonZeroRule(1);

        [Theory]
        [InlineData("-20")]
        [InlineData("0")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string seats)
        {
            var result = rule.Evaluate(seats);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_NON_ZERO_FOR_NO_OF_TICKETS);
        }

        [Theory]
        [InlineData("20")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string seats)
        {
            var result = rule.Evaluate(seats);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
