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
using CinemaBookingCore.Utilities;
using CinemaBookingCore.States;
using CinemaBookingCore.Services;

namespace CinemaBookingTests.RuleEngineTests
{
    public class ChangeSeatRuleEngineTests
    {
        RuleEngine ruleEngine = new ReserveBookingStateFactory().CreateRuleEngine();
        private CinemaContext cinemaContext = new CinemaContext();

        public void SetUp()
        {
            var cinema = new CinemaBookingCore.Models.Cinema { SeatsPerRow = 10 , Rows=10};
            cinema.SeatingMap = SeatingMapService.Instance.BuildSeatingMap(cinema.Rows, cinema.SeatsPerRow);
            cinemaContext.SetCinema(cinema);
            cinemaContext.RowNameToRowNoMap = Helper.BuildRowNameToRowNoMap(MAX_ROWS);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("@13")]
        [InlineData("abcd")]
        [InlineData("F100")]
        [InlineData("AB1")]
        [InlineData("F11")]
        [InlineData("F0")]
        [InlineData("F-5")]
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {
            SetUp();

            var result = ruleEngine.Run(input, cinemaContext);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("F10")]
        [InlineData("f10")]
        [InlineData("A1")]
        [InlineData("a01")]

        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            SetUp();

            var result = ruleEngine.Run(input, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
