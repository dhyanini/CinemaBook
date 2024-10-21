using CinemaBookingCore.Validations.Rules;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.RulesTests
{
    public class SeatingMapValidParameterRuleTests
    {
        private SeatingMapValidParameterRule rule = new SeatingMapValidParameterRule(1);

        [Theory]
        [InlineData("")]
        [InlineData ("OnlyOneArgument")]
        [InlineData ("FirstArgument  SecondArgument")]
        public void Evaluate_WhenInvalidParamPassed_ReturnsFalse(string input)
        {
            var result = rule.Evaluate(input);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_INPUT_3PARAMETERS);
        }

        [Theory]
        [InlineData("FirstArgument  SecondArgument  ThirdArgument")]
        public void Evaluate_WhenValidParamPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
