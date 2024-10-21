using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Validations.Rules;
using Xunit;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;

namespace CinemaBookingCore.Validations.ReserveBookingRules
{
    public class SelectedSeatAlphabetCheckRuleTests
    {
        private SelectedSeatAlphabetCheckRule rule = new SelectedSeatAlphabetCheckRule(1);
        private CinemaContext cinemaContext = new CinemaContext();
        public SelectedSeatAlphabetCheckRuleTests()
        {
            cinemaContext.RowNameToRowNoMap = Helper.BuildRowNameToRowNoMap(MAX_ROWS);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("@13")]

        public void Evaluate_WhenInvalidInputPassed_ReturnsFalse(string input)
        {

            var result = rule.Evaluate(input, cinemaContext);
            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData("F10")]
        [InlineData("f10")]
        [InlineData("A1")]
        [InlineData("a01")]

        public void Evaluate_WhenValidInputPassed_ReturnsTrue(string input)
        {
            var result = rule.Evaluate(input, cinemaContext);
            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }
    }
}
