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

namespace CinemaBookingTests.RulesTests.ReserveBookingRulesTests
{
    public class SelectedSeatTotalSeatsAvailableRuleTests
    {
        RuleEngine ruleEngine = new ReserveBookingStateFactory().CreateRuleEngine();
        private CinemaContext cinemaContext = new CinemaContext();

        public void SetUp()
        {
            var cinema = new CinemaBookingCore.Models.Cinema { SeatsPerRow = 10, Rows = 10 };
            cinema.SeatingMap = SeatingMapService.Instance.BuildSeatingMap(cinema.Rows, cinema.SeatsPerRow);
            cinemaContext.SetCinema(cinema);
            cinemaContext.RowNameToRowNoMap = Helper.BuildRowNameToRowNoMap(cinema.Rows);
        }

        [Theory]
        [InlineData("I5", "20")]
        [InlineData("J1", "11")]   
        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input, string seatsToBook)
        {
            SetUp();
            cinemaContext.SetTotalSeatsToBook(int.Parse(seatsToBook));

            var result = ruleEngine.Run(input, cinemaContext);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("D5","20")]
        [InlineData("A10","11")]
        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input, string seatsToBook)
        {
            SetUp();
            cinemaContext.SetTotalSeatsToBook(int.Parse(seatsToBook));

            var result = ruleEngine.Run(input, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
