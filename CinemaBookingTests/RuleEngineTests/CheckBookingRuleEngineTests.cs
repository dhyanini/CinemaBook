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
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingTests.RuleEngineTests
{    
    public class CheckBookingRuleEngineTests
    {
        RuleEngine ruleEngine = new CheckBookingStateFactory().CreateRuleEngine();

        [Theory]
        [InlineData("GIC0001")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string bookingId)
        {
            CinemaContext cinemaContext = new CinemaContext();
            var result = ruleEngine.Run(bookingId, cinemaContext);
            Assert.False(result.IsValid);
            Assert.Equal(result.ErrorMessage, ERROR_MESSAGE_BOOKING_NOT_EXISTS);

        }

        [Theory]
        [InlineData("GIC0001")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string bookingId)
        {
            CinemaContext cinemaContext = new CinemaContext();
            cinemaContext.BookingIdToSeatingMap["GIC0001"] = new List<CinemaBookingCore.Models.Booking>();
            var result = ruleEngine.Run(bookingId, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }


    }
}
