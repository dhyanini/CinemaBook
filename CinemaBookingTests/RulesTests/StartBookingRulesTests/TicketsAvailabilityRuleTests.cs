using CinemaBookingCore.States;
using CinemaBookingCore.Validations.Rules;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.RulesTests.StartBookingRulesTests
{
    public class TicketsAvailabilityRuleTests
    {
        private TicketsAvailabilityRule rule = new TicketsAvailabilityRule(1);

        [Theory]
        [InlineData("20", 10)] 
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string seats, int availabileSeats)
        {
            Mock<CinemaContext> cinemaContext = new Mock<CinemaContext>();
            cinemaContext.Setup(c => c.Cinema.Available).Returns(availabileSeats);
            var result = rule.Evaluate(seats, cinemaContext.Object);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("20", 21)]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string seats, int availabileSeats)
        {
            Mock<CinemaContext> cinemaContext = new Mock<CinemaContext>();
            cinemaContext.Setup(c => c.Cinema.Available).Returns(availabileSeats);
            var result = rule.Evaluate(seats, cinemaContext.Object);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

    }
}
