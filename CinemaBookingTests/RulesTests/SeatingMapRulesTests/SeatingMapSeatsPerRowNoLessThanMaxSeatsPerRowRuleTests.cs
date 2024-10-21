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
    public class SeatingMapSeatsPerRowNoLessThanMaxSeatsPerRowRuleTests
    {

        private SeatingMapSeatsPerRowNoLessThanMaxSeatsPerRowRule rule = new SeatingMapSeatsPerRowNoLessThanMaxSeatsPerRowRule(1);

        [Theory]
        [InlineData("Inception  5 51")]
        public void Evaluate_WhenInvalidRowNoPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_MAX_SEATS_PER_ROW);
        }

        [Theory]
        [InlineData("Inception  1  20")]
        [InlineData("Inception  26  20")]

        public void Evaluate_WhenValidRowNoPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
