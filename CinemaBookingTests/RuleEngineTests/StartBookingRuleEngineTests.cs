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
    public class StartBookingRuleEngineTests
    {

        RuleEngine ruleEngine = new StartBookingStateFactory().CreateRuleEngine();

        [Theory]
        [InlineData("invalid", 10)]
        [InlineData("-20", 10)]
        [InlineData("0", 10)]
        [InlineData("20", 10)]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input, int availabileSeats)
        {
            Mock<CinemaContext> cinemaContext = new Mock<CinemaContext>();
            cinemaContext.Setup(c => c.Cinema.Available).Returns(availabileSeats);
            var result = ruleEngine.Run(input, cinemaContext.Object);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("", 21)]
        [InlineData("20", 21)]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input, int availabileSeats)
        {
            Mock<CinemaContext> cinemaContext = new Mock<CinemaContext>();
            cinemaContext.Setup(c => c.Cinema.Available).Returns(availabileSeats);
            var result = ruleEngine.Run(input, cinemaContext.Object);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

    }
}
