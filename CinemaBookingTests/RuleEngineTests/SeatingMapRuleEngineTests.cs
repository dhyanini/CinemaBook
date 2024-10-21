using CinemaBookingCore.Factories;
using CinemaBookingCore.Validations.RuleEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CinemaBookingTests.RuleEngineTests
{
    public class SeatingMapRuleEngineTests
    {
        RuleEngine ruleEngine = new SeatingMapStateFactory().CreateRuleEngine();

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
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            var result = ruleEngine.Run(input);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("Inception  10  20")]
        [InlineData("Inception  26  20")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = ruleEngine.Run(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
