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
    public class GoBackRuleTests
    {
        private GoBackRule rule = new GoBackRule(1);

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        public void Evaluate_WhenInputPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input);
            Assert.True(result.IsValid);
        }
    }
}
