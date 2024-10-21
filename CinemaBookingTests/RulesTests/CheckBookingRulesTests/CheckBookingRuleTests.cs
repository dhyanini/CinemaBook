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


namespace CinemaBookingTests.RulesTests.CheckBookingRulesTests
{
    public class CheckBookingRuleTests
    {
        private CheckBookingRule rule = new CheckBookingRule(1);

        [Theory]
        [InlineData("GIC0001")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string bookingId)
        {
            CinemaContext cinemaContext = new CinemaContext();
            var result = rule.Evaluate(bookingId, cinemaContext);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_BOOKING_NOT_EXISTS);

        }

        [Theory]
        [InlineData("GIC0001")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string bookingId)
        {
            CinemaContext cinemaContext = new CinemaContext();
            cinemaContext.BookingIdToSeatingMap["GIC0001"] = new List<CinemaBookingCore.Models.Booking>();
            var result = rule.Evaluate(bookingId, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
