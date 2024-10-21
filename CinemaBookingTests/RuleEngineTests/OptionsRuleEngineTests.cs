using CinemaBookingCore.Factories;
using CinemaBookingCore.Validations.RuleEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CinemaBookingCore.States;


namespace CinemaBookingTests.RuleEngineTests
{
    public class OptionsRuleEngineTests
    {
        RuleEngine ruleEngine = new OptionsStateFactory().CreateRuleEngine();

        [Theory]
        [InlineData("invalid")]
        [InlineData("-1")]
        [InlineData("4")]
        [InlineData("0")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            var result = ruleEngine.Run(input);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = ruleEngine.Run(input);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
