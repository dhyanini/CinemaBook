using CinemaBookingCore.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;


namespace CinemaBookingTests.RulesTests.OptionsRulesTests
{
    public class IsNumberRuleTests
    {
        private IsNumberRule rule = new IsNumberRule(1);


        [Theory]
        [InlineData("invalid")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_VALID_INPUT_NO);
        }

        [Theory]
        [InlineData("20")]
        [InlineData("0")]

        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

    }
}
