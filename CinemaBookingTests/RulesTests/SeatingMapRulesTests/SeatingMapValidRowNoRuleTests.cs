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
    public class SeatingMapValidRowNoRuleTests
    {
        private SeatingMapValidRowNoRule rule = new SeatingMapValidRowNoRule(1);

        [Theory]
       
        [InlineData("Inception  abc 20")]
        public void Evaluate_WhenInvalidRowNoPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_VALID_ROW_NO);
        }

        [Theory]
        [InlineData("Inception  10  20")]
        public void Evaluate_WhenValidRowNoPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
